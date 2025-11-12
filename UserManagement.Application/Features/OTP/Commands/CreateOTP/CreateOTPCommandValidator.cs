using FluentValidation;
using UserManagement.Application.Identity;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.OTP.Commands.CreateOTP
{
    internal class CreateOTPCommandValidator : AbstractValidator<CreateOTPCommand>
    {
        public CreateOTPCommandValidator(CustomUserManager userManager)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;
            
            RuleFor(x => x.Type)
                .NotNull().WithMessage(Messages.EmptyField)
                .IsInEnum().WithMessage(Messages.IncorrectData);
            
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EmailAddress().WithMessage(Messages.InvalidEmailAddress)
                //.MustAsync(((email, token) => userManager.IsUserExistByEmailAsync(email)))
                .WithMessage(Messages.NotFound);
        }
    }
}
