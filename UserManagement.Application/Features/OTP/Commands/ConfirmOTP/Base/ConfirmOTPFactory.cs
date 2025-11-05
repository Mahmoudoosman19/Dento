using UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Abstract;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Base
{
    internal class ConfirmOTPFactory : IConfirmOTPFactory
    {
        private readonly IEnumerable<BaseConfirmOTP> _baseConfirmOTPs;

        public ConfirmOTPFactory(IEnumerable<BaseConfirmOTP> baseConfirmOTPs)
        {
            _baseConfirmOTPs = baseConfirmOTPs;
        }

        public BaseConfirmOTP ConfirmOTP(OTPType type)
        {
            var confirmOTP = _baseConfirmOTPs.FirstOrDefault(r => r.Type == type);

            return confirmOTP!;
        }
    }
}
