using Common.Domain.Repositories;
using FluentValidation;
using UserManagement.Application.Identity;
using UserManagement.Application.Specifications.User;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommandValidation
    : AbstractValidator<ForgotPasswordCommand>
{
    private readonly CustomUserManager _userManager;
    private readonly IGenericRepository<Domain.Entities.User> _userRepo;
    private Domain.Entities.User? _user;
    
    public ForgotPasswordCommandValidation(CustomUserManager userManager, IGenericRepository<Domain.Entities.User> userRepo)
    {
        _userManager = userManager;
        _userRepo = userRepo;

        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage(Messages.EmptyField);
        
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage(Messages.EmptyField);
        
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .MustAsync(UserExists).WithMessage(Messages.NotFound);
        
        RuleFor(x => x)
            .CustomAsync(ValidatePasswords);

        RuleFor(x => x)
            .CustomAsync(ValidateOtp);
    }

    private async Task<bool> UserExists(string email, CancellationToken cancellationToken)
    {
        GetUser(email, cancellationToken);
        
        return _user is not null;
    }

    private async Task ValidateOtp(ForgotPasswordCommand command, ValidationContext<ForgotPasswordCommand> context,
        CancellationToken cancellationToken)
    {
        GetUser(command.Email, cancellationToken);
        
        var otp = _user!.Otp;

        if (otp is null)
        {
            context.AddFailure("OTP",Messages.InvalidOTP);
            return;
        }

        if (!otp.IsUsed && otp.ExpireOn > DateTime.UtcNow)
            return;
        else
            context.AddFailure("OTP", Messages.NotFound);
    }
    
    private async Task ValidatePasswords(ForgotPasswordCommand command, ValidationContext<ForgotPasswordCommand> context, CancellationToken cancellationToken)
    {
        GetUser(command.Email, cancellationToken);
        
        if (!command.ConfirmPassword.Equals(command.NewPassword))
        {
            context.AddFailure(Messages.PasswordsNotMatch);
            return;
        }
        
        var validateResult = await _userManager.validatePassword(_user!, command.NewPassword);
        if (!validateResult.Succeeded)
        {
            var errors = validateResult.Errors.Select(e => e.Description);
            var errorString = string.Join('\n', errors);
            context.AddFailure(nameof(command.NewPassword), errorString);
        }
    }
    
    private void GetUser(string email, CancellationToken cancellationToken)
    {
        if (_user is null)
            _user = _userRepo.GetEntityWithSpec(new GetUserByEmailWithOtpSpecification(email));
    }
}