using FluentValidation;
using UserManagement.Application.Identity;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Auth.Commands.ChangePhoneNumber
{
    internal class ChangePhoneNumberCommandValidator : AbstractValidator<ChangePhoneNumberCommand>
    {
        private readonly CustomUserManager _userManager;
        public ChangePhoneNumberCommandValidator(CustomUserManager userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.PhoneNumber)
                  .NotEmpty().WithMessage(Messages.EmptyField)
                  .Must(IsPhoneNumExit).WithMessage(Messages.PhoneNumberAlreadyUsed);
        }

        private bool IsPhoneNumExit(string PhoneNumber)
        {
            var user = _userManager.Users.Where(u => u.PhoneNumber == PhoneNumber).FirstOrDefault();
            return user == null;
        }
    }
}