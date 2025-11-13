namespace UserManagement.Application.Features.Notifications.Command.CreateNotification
{
    internal class CreateNotificationsCommandValidator : AbstractValidator<CreateNotificationsCommand>
    {
        private readonly CustomUserManager _userManager;
        public CreateNotificationsCommandValidator(CustomUserManager userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .Must(IsUserIdExist).WithMessage(Messages.NotFound);

            RuleFor(x => x.Title)
            .NotEmpty().WithMessage(Messages.EmptyField);

            RuleFor(x => x.Content)
          .NotEmpty().WithMessage(Messages.EmptyField);
        }
        private bool IsUserIdExist(Guid UserId)
        {
            var user = _userManager.Users.Where(u => u.Id == UserId).FirstOrDefault();
            return user == null;
        }
    }
}
