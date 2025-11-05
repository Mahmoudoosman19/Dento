using FluentValidation;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.User.Commands.UpdateUserProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            ValidationRules();
        }

        private void ValidationRules()
        {
            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .WithMessage(Messages.IncorrectData);

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .WithMessage(Messages.IncorrectData);
        }
    }
}
