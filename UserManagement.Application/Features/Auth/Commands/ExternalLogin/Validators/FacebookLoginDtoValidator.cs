using UserManagement.Application.Features.Auth.Commands.ExternalLogin.DTOs;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin.Validators;

public class FacebookLoginDtoValidator
    : AbstractValidator<FacebookLoginDto>
{
    public FacebookLoginDtoValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty().WithMessage(Messages.EmptyField);
    }
}