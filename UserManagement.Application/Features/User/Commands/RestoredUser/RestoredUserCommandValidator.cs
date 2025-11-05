using FluentValidation;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.User.Commands.RestoredUser
{
    public class RestoredUserCommandValidator : AbstractValidator<RestoredUserCommand>
    {
        public RestoredUserCommandValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty()
                .WithMessage(Messages.EmptyField);
        }
    }
}
