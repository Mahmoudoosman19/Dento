using Common.Domain.Repositories;
using Common.Domain.Shared;
using UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Abstract;
using UserManagement.Application.Identity;
using UserManagement.Application.Specifications.User;
using UserManagement.Domain.Enums;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Type
{
    internal class ConfirmUserEmailType : BaseConfirmOTP
    {
        public ConfirmUserEmailType(CustomUserManager userManager,
            IGenericRepository<Domain.Entities.OTP> OTPRepo,
            IGenericRepository<Domain.Entities.User> userRepo) : base(userManager, OTPRepo, userRepo)
        {
        }

        public override OTPType Type { get; set; } = OTPType.ConfirmEmail;

        public override async Task<ResponseModel> ConfirmOTP(ConfirmOTPCommand command)
        {
            var user = _userRepo.GetEntityWithSpec(new GetUserByEmailWithOtpSpecification(command.Email!));
            
            var otp = user!.Otp;

            user.ConfirmEmail();
            await _userManager.UpdateAsync(user);

            otp.MarkAsUsed();

            await _userRepo.SaveChangesAsync();

            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}