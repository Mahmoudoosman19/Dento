using FluentValidation;
using UserManagement.Application.Features.Auth.Commands.Register.DTOs;
using UserManagement.Application.Identity;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Auth.Commands.Register.Validators
{
    internal class BaseRegisterDtoValidator : AbstractValidator<BaseRegisterDto>
    {
        private readonly CustomUserManager _userManager;
        public BaseRegisterDtoValidator(CustomUserManager userManager)
        {
            _userManager = userManager;
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .Must(IsUserNameExit).WithMessage(Messages.RedundantData);

            RuleFor(x => x.FullNameEn)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsEnglish().WithMessage(Messages.IncorrectData);

            RuleFor(x => x.FullNameAr)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsArabic().WithMessage(Messages.IncorrectData);

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsEnglish(combineNumbers: true).WithMessage(Messages.InvalidEmailAddress)
                .EmailAddress().WithMessage(Messages.InvalidEmailAddress)
                .MustAsync(IsEmailExit).WithMessage(Messages.thisemailisalreadyexist);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .Must(IsPhoneNumExit).WithMessage(Messages.PhoneNumberAlreadyUsed);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Messages.EmptyField);

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .IsInEnum().WithMessage(Messages.IncorrectData);
        }
        private bool IsUserNameExit(string useName)
        {
            var user = _userManager.Users.Where(u => u.UserName == useName).FirstOrDefault();
            return user == null;

        }
        private async Task<bool> IsEmailExit(string Email, CancellationToken cancellationToken)
        {
            return !await _userManager.IsUserExistByEmailAsync(Email);

        }
        private bool IsPhoneNumExit(string PhoneNumber)
        {
            var user = _userManager.Users.Where(u => u.PhoneNumber == PhoneNumber).FirstOrDefault();
            return user == null;

        }
    }
}
