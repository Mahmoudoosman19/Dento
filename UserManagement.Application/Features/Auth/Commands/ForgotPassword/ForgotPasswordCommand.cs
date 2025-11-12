using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommand : ICommand
{
    public string Email { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;

}