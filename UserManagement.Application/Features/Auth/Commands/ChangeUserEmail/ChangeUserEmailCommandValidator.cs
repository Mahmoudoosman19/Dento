using FluentValidation;
using UserManagement.Application.Identity;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Auth.Commands.ChangeUserEmail
{
    internal class ChangeUserEmailCommandValidator : AbstractValidator<ChangeUserEmailCommand>
    {
        private readonly CustomUserManager _userManager;
        public ChangeUserEmailCommandValidator(CustomUserManager userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Email)
               .Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage(Messages.EmptyField)
               .IsEnglish(combineNumbers: true).WithMessage(Messages.InvalidEmailAddress)
               .EmailAddress().WithMessage(Messages.InvalidEmailAddress)
               .MustAsync(IsEmailExit).WithMessage(Messages.thisemailisalreadyexist);
        
        }
        private async Task<bool> IsEmailExit(string Email, CancellationToken cancellationToken)
        {
            return !await _userManager.IsUserExistByEmailAsync(Email);
        }
    }
}
