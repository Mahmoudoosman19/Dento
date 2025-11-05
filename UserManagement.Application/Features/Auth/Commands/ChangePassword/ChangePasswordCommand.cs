using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommand : ICommand
    {
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
