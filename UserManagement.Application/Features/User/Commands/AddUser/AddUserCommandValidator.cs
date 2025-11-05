using FluentValidation;
using UserManagement.Application.Features.User.Commands.AddUser;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.User.Commands.UpdateUserProfile
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
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
