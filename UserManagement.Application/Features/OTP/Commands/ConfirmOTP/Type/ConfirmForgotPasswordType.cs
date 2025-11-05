using Common.Domain.Repositories;
using Common.Domain.Shared;
using Microsoft.Extensions.Options;
using UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Abstract;
using UserManagement.Application.Identity;
using UserManagement.Application.Specifications.OTP;
using UserManagement.Domain.Enums;
using UserManagement.Domain.Options;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Type;

internal class ConfirmForgotPasswordType : BaseConfirmOTP
{
    private readonly IOptions<OTPOptions> _otpOptions;

    public ConfirmForgotPasswordType(CustomUserManager userManager,
        IGenericRepository<Domain.Entities.OTP> otpRepo,
        IGenericRepository<Domain.Entities.User> userRepo,
        IOptions<OTPOptions> otpOptions) : base(userManager, otpRepo, userRepo)
    {
        _otpOptions = otpOptions;
    }

    public override OTPType Type { get; set; } = OTPType.ForgotPassword;
    public override async Task<ResponseModel> ConfirmOTP(ConfirmOTPCommand command)
    {
        var otp = _otpRepo.GetEntityWithSpec(new GetOTPByCodeSpecification(command.Code));

        var expirationTime = DateTime.UtcNow.AddMinutes(_otpOptions.Value.ExpirationTimeInMinutes);
        otp!.MarkAsUsed();
        otp.SetExpireOn(expirationTime);

        await _userRepo.SaveChangesAsync();

        return ResponseModel.Success(Messages.SuccessfulOperation);
    }
}