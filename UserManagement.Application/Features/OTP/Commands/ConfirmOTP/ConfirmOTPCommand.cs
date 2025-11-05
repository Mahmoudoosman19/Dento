using Common.Application.Abstractions.Messaging;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.OTP.Commands.ConfirmOTP
{
    public class ConfirmOTPCommand : ICommand
    {
        public string Code { get; init; } = null!;
        public string? Email { get; set; }
        public OTPType Type { get; init; }
    }
}
