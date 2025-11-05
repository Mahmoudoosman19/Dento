using FluentValidation;
using IdentityHelper.Abstraction;
using UserManagement.Application.Identity;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Auth.Commands.ChangePassword
{
    internal class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        private readonly CustomUserManager _userManager;
        private readonly ITokenExtractor _tokenExtractor;
        const string passwordPattern = "/^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).{6,}$/";

        private Domain.Entities.User? _user;
        public ChangePasswordValidator(CustomUserManager userManager, ITokenExtractor tokenExtractor)
        {
            _userManager = userManager;
            _tokenExtractor = tokenExtractor;

            RuleFor(x => x.CurrentPassword)
              .NotEmpty().WithMessage(Messages.EmptyField);
            RuleFor(x => x.NewPassword)
             .NotEmpty().WithMessage(Messages.EmptyField);
            RuleFor(x => x.ConfirmPassword)
             .NotEmpty().WithMessage(Messages.EmptyField);
            RuleFor(x => x)
                .CustomAsync(PasswordCorrectAsync);
            RuleFor(x => x)
              .CustomAsync(ValidatePasswordsMatch);
        }
        private async Task GetUser()
        {
            if (_user is null)
            {
                var userId = _tokenExtractor.GetUserId();
                _user = await _userManager.FindByIdAsync(userId.ToString());
            }
        }
        private async Task PasswordCorrectAsync(ChangePasswordCommand Command, ValidationContext<ChangePasswordCommand> context, CancellationToken cancellationToken)
        {
            await GetUser();
            if (_user is not null)
            {
                var result = await _userManager.CheckPasswordAsync(_user, Command.CurrentPassword);
                if (!result)
                    context.AddFailure(Messages.IncorrectCurrentPassword);
            }
        }
        private async Task ValidatePasswordsMatch(ChangePasswordCommand Command, ValidationContext<ChangePasswordCommand> context, CancellationToken cancellationToken)
        {
            await GetUser();
            if (_user is not null)
            {
                if (!Command.ConfirmPassword.Equals(Command.NewPassword))
                {
                    context.AddFailure(Messages.PasswordsNotMatch);
                    return;
                }
                
                var validateResult = await _userManager.validatePassword(_user, Command.NewPassword);
                if (!validateResult.Succeeded)
                {
                    var errors = validateResult.Errors.Select(e => e.Description);
                    var errorString = string.Join('\n', errors);
                    context.AddFailure(nameof(Command.NewPassword), errorString);
                    return;
                }

            }

        }

    }
}
