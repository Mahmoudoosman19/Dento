using FluentValidation;
using UserManagement.Application.Identity;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.User.Commands.ToggleAccountActivation
{
    internal class ToggleActivationCommandValidator : AbstractValidator<ToggleUserAccountActivationCommand>
    {
        private readonly CustomUserManager _userManager;

        public ToggleActivationCommandValidator(CustomUserManager userManager)
        {
            _userManager = userManager;

            RuleFor(user => user.Id)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .MustAsync(UserExist).WithMessage(Messages.NotFound);
        }

        private async Task<bool> UserExist(Guid id, CancellationToken cancellationToken)
            => await _userManager.IsUserExistByIdAsync(id);
    }
}
