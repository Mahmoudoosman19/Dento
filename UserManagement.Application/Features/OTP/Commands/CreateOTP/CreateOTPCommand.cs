using Common.Application.Abstractions.Messaging;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.OTP.Commands.CreateOTP
{
    public class CreateOTPCommand : ICommand
    {
        public OTPType Type { get; init; }

        public string Email { get; set; } = null!;
        public string? Purpose { get; init; }
    }
}