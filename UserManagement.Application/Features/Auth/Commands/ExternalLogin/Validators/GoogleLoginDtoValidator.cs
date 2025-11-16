using UserManagement.Application.Features.Auth.Commands.ExternalLogin.DTOs;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin.Validators;

internal class GoogleLoginDtoValidator
    : AbstractValidator<GoogleLoginDto>
{
    public GoogleLoginDtoValidator()
    {
        RuleFor(x => x.IdToken)
            .NotEmpty().WithMessage(Messages.EmptyField);
    }
}