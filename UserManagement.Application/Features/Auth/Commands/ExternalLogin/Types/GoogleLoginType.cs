using Common.Domain.Shared;
using Google.Apis.Auth;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using UserManagement.Application.Abstractions;
using UserManagement.Application.DTOs;
using UserManagement.Application.Features.Auth.Commands.ExternalLogin.Abstract;
using UserManagement.Application.Features.Auth.Commands.Login;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin.Types;

internal class GoogleLoginType : BaseExternalLogin
{
    private readonly CustomUserManager _userManager;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public GoogleLoginType(CustomUserManager userManager, IJwtProvider jwtProvider, IMapper mapper, IConfiguration configuration) 
        : base(userManager, jwtProvider, mapper)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
        _configuration = configuration;
    }
    public override LoginProvider LoginProvider { get; set; } = LoginProvider.Google;
    
    public override async Task<ResponseModel<LoginCommandResponse>> Login(ExternalLoginCommand command)
    {
        var payload = await VerifyGoogleIdTokenAsync(command.GoogleLoginDto!.IdToken);
        if (payload == null)
        {
            return ResponseModel.Failure<LoginCommandResponse>("Invalid Id Token");
        }

        var email = payload.Email;
        var fullName = payload.Name;

        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null)
        {
            user = CreateCustomerUser(email, fullName);

            var result = await _userManager.CreateExternalLoginUser(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine,
                    result.Errors.Select(e => e.Description));

                return ResponseModel.Failure<LoginCommandResponse>(errors);
            }
        }

        var token = await _jwtProvider.Generate(user);
        
        return new LoginCommandResponse()
        {
            Token = token,
            User = _mapper.Map<UserDto>(user!)
        };
    }
    
    private async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleIdTokenAsync(string idToken)
    {
        var clientId = _configuration["GoogleLogin:ClientId"];
        
        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { clientId }
        };

        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            return payload;
        }
        catch
        {
            return null;
        }
    }
}