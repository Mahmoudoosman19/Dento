using UserManagement.Application.Features.Auth.Commands.ExternalLogin.Validators;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin;

internal class ExternalLoginCommandValidator
    : AbstractValidator<ExternalLoginCommand>
{
    public ExternalLoginCommandValidator()
    {
        RuleFor(x => x.LoginProvider)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .IsInEnum().WithMessage(Messages.IncorrectData);

        When(x => x.LoginProvider == LoginProvider.Google, (() =>
        {
            RuleFor(x => x.GoogleLoginDto)
                .NotNull().WithMessage(Messages.EmptyField)
                .SetValidator(new GoogleLoginDtoValidator()!);
        }));
        
        When(x => x.LoginProvider == LoginProvider.Facebook, (() =>
        {
            RuleFor(x => x.FacebookLoginDto)
                .NotNull().WithMessage(Messages.EmptyField)
                .SetValidator(new FacebookLoginDtoValidator()!);
        }));  
        
    }
}