using Common.Domain.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using UserManagement.Application.Features.Auth.Commands.Register.DTOs;
using UserManagement.Application.Identity;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Auth.Commands.Register.Validators
{
    internal class DesignerRegisterDtoValidator : AbstractValidator<DesignerRegisterDto>
    {
        private readonly CustomUserManager _userManager;
        private readonly IGenericRepository<Domain.Entities.Designer> _designerRepo;
        public DesignerRegisterDtoValidator(CustomUserManager userManager)
        {
            _userManager = userManager;
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .Must(IsUserNameExit).WithMessage(Messages.RedundantData);

            RuleFor(x => x.FullNameEn)
                .NotEmpty().WithMessage(Messages.EmptyField);

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
            _userManager = userManager;
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
