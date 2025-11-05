using Common.Domain.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Specifications.RolePermission;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Options;

namespace UserManagement.Infrastructure.Authentication;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly IGenericRepository<Role> _roleRepo;
    private readonly IGenericRepository<RolePermission> _rolePermissionRepo;

    public JwtProvider(IOptions<JwtOptions> options,
        IGenericRepository<Role> roleRepo,
        IGenericRepository<RolePermission> rolePermissionRepo)
    {
        _options = options.Value;
        _roleRepo = roleRepo;
        _rolePermissionRepo = rolePermissionRepo;
    }

    public async Task<string> Generate(User user)
    {

        var userRole = await _roleRepo.GetByIdAsync((long)user.RoleId!);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email!.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),
            new("Role", userRole.NameEn!.ToString())
        };


        if (userRole is null)
            throw new ArgumentNullException("user role not founded");


        if (userRole.NameEn.ToLower().Equals("admin"))
        {
            claims.Add(new Claim("Permissions", "Admin"));
        }
        else
        {
            (var rolePermissions, int count) = _rolePermissionRepo
                    .GetWithSpec(new GetRolePermissionByRoleIdSpecification((long)user.RoleId!));

            claims.AddRange(rolePermissions.ToList().Select(rp => new Claim("Permissions", rp.Permission!.NameEn)));
        }

        var dict = JwtSecurityTokenHandler.DefaultInboundClaimTypeMap;
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
