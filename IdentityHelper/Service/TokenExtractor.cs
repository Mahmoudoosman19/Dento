using IdentityHelper.Abstraction;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace IdentityHelper.Service
{
    internal class TokenExtractor : ITokenExtractor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenExtractor(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Guid GetUserId()
        {
            var claim = GetClaim(JwtRegisteredClaimNames.Sid);
            return Guid.Parse(claim);
        }

        public string GetUserRole()
        {
            var claimValue = _contextAccessor.HttpContext.User.FindFirst("Role")?.Value;
            if(claimValue is not null)
                return claimValue;
            return GetClaim("Role");
        }

        public string GetEmail()
        {
            return GetClaim(JwtRegisteredClaimNames.Email);
        }

        public string GetUsername()
        {
            return GetClaim(JwtRegisteredClaimNames.UniqueName);
        }

        public List<string> GetPermissions()
        {
            return ClaimsIdentity()
                .FindAll("Permissions")
                .Select(c => c.Value)
                .ToList();
        }

        public bool IsUserAuthenticated()
        {
            if (_contextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var token = authHeader.ToString().Split(' ').Last();
                return !string.IsNullOrWhiteSpace(token);
            }
            return false;
        }


        private string GetClaim(string claimType)
        {
            var identity = ClaimsIdentity();
            var claim = identity.FindFirst(claimType);
            return claim?.Value ?? throw new InvalidOperationException($"Claim '{claimType}' not found.");
        }

        private ClaimsIdentity ClaimsIdentity()
        {
            if (_contextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var token = authHeader.ToString().Split(' ').Last();
                var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
                return new ClaimsIdentity(claims);
            }
            else
            {
                throw new UnauthorizedAccessException("Authorization token is missing or invalid.");
            }
        }
    }
}
