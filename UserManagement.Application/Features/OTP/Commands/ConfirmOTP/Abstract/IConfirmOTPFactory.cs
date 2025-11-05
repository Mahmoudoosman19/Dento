using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Abstract
{
    internal interface IConfirmOTPFactory
    {
        BaseConfirmOTP ConfirmOTP(OTPType type);
    }
}
