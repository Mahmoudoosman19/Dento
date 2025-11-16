using Common.Application.Abstractions.Messaging;
using UserManagement.Application.Features.Auth.Commands.ExternalLogin.DTOs;
using UserManagement.Application.Features.Auth.Commands.Login;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin;

public class ExternalLoginCommand : ICommand<LoginCommandResponse>
{
    public LoginProvider LoginProvider { get; set; }

    public GoogleLoginDto? GoogleLoginDto { get; set; }
    public FacebookLoginDto? FacebookLoginDto { get; set; }
}