using Common.Domain.Repositories;
using Common.Domain.Shared;
using UserManagement.Application.Identity;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Abstract
{
    internal abstract class BaseConfirmOTP
    {
        protected readonly CustomUserManager _userManager;
        protected readonly IGenericRepository<Domain.Entities.OTP> _otpRepo;
        protected readonly IGenericRepository<Domain.Entities.User> _userRepo;

        protected BaseConfirmOTP(
            CustomUserManager userManager,
            IGenericRepository<Domain.Entities.OTP> OTPRepo,
            IGenericRepository<Domain.Entities.User> userRepo)
        {
            _userManager = userManager;
            _otpRepo = OTPRepo;
            _userRepo = userRepo;
        }

        public abstract OTPType Type { get; set; }

        public abstract Task<ResponseModel> ConfirmOTP(ConfirmOTPCommand command);
    }
}
