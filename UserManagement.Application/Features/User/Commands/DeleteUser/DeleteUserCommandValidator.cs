using FluentValidation;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty()
                .WithMessage(Messages.EmptyField);
        }
    }
}
