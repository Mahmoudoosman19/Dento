using FluentValidation;
using UserManagement.Application.Identity;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Auth.Commands.Login
{
    internal class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        private readonly CustomUserManager _userManager;
        private Domain.Entities.User? _user;

        public LoginCommandValidator(CustomUserManager userManager)
        {
            _userManager = userManager;
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EmailAddress().WithMessage(Messages.InvalidEmailAddress)
                .CustomAsync(UserExistAsync)
                .MustAsync(UserNotLockedOutAsync).WithMessage(Messages.YourAccountHasBeenLockedOut)
                .MustAsync(EmailConfirmedAsync).WithMessage(Messages.YourEmailNotConfirmedYet)
                .MustAsync(UserActiveAsync).WithMessage(Messages.YourAccountHasBeenDeactivated)
                .MustAsync(UserNotBlockedAsync).WithMessage(Messages.YourAccountHasBeenBlocked);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Messages.EmptyField);

            RuleFor(x => x)
                .CustomAsync(PasswordCorrectAsync);
        }

        private async Task UserExistAsync(string email, ValidationContext<LoginCommand> context, CancellationToken cancellationToken)
        {
            await GetUser(email);
            if (_user is null)
                context.AddFailure("", Messages.IncorrectEmailOrPassword);
        }

        private async Task<bool> EmailConfirmedAsync(string email, CancellationToken cancellationToken)
        {
            await GetUser(email);
            return _user != null && await _userManager.IsEmailConfirmedAsync(_user);
        }

        private async Task<bool> UserNotLockedOutAsync(string email, CancellationToken cancellationToken)
        {
            await GetUser(email);
            return _user != null && !await _userManager.IsLockedOutAsync(_user);
        }

        private async Task<bool> UserActiveAsync(string email, CancellationToken cancellationToken)
        {
            await GetUser(email);
            return _user != null && _userManager.IsUserActive(_user);
        }

        private async Task<bool> UserNotBlockedAsync(string email, CancellationToken cancellationToken)
        {
            await GetUser(email);
            return _user != null && !_userManager.IsUserBlocked(_user);
        }

        private async Task PasswordCorrectAsync(LoginCommand loginCommand, ValidationContext<LoginCommand> context, CancellationToken cancellationToken)
        {
            await GetUser(loginCommand.Email);
            if (_user is not null)
            {
                var result = await _userManager.CheckPasswordAsync(_user, loginCommand.Password);
                if (!result)
                    context.AddFailure(Messages.IncorrectEmailOrPassword);
            }
        }

        private async Task GetUser(string email)
        {
            if (_user is null)
                _user = await _userManager.FindByEmailAsync(email);
        }
    }
}
