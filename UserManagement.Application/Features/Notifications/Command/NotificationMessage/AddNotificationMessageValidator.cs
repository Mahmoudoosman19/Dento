using FluentValidation;
using System.Text.RegularExpressions;
using System.Globalization;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Notifications.Command.NotificationMessage
{
    public class AddNotificationMessageValidator : AbstractValidator<AddNotificationMessageCommand>
    {
        public AddNotificationMessageValidator()
        {
            RuleFor(c => c.ResourceValueEnglish)
               .NotEmpty().WithMessage(Messages.EmptyField)
               .Must((c, value) => HaveValidPlaceholders(value, LanguageEnum.en))
               .WithMessage(Messages.InvalidPlaceholderFormat)
                .Must(value => IsCorrectLanguage(value, LanguageEnum.en))
                .WithMessage(Messages.IncorrectData);

            RuleFor(c => c.ResourceValueArbice)
               .NotEmpty().WithMessage(Messages.EmptyField)
               .Must((c, value) => HaveValidPlaceholders(value, LanguageEnum.ar))
               .WithMessage(Messages.InvalidPlaceholderFormat).
               Must(value => IsCorrectLanguage(value, LanguageEnum.ar))
               .WithMessage(Messages.IncorrectData);

            RuleFor(c => c.ResourceKey)
              .NotEmpty().WithMessage(Messages.EmptyField);
        }

        private bool HaveValidPlaceholders(string message, LanguageEnum language)
        {
            if (string.IsNullOrWhiteSpace(message)) return false;

            var matches = Regex.Matches(message, @"\{\d+\}");
            var numbers = matches.Select(m => int.Parse(m.Value.Trim('{', '}'))).ToList();

            if (!numbers.Any()) return false; 
            if (!numbers.OrderBy(n => n).SequenceEqual(Enumerable.Range(0, numbers.Count))) return false;

            return true;
        }

       
        private bool IsCorrectLanguage(string message, LanguageEnum language)
        {
            if (string.IsNullOrWhiteSpace(message)) return false;

            bool containsArabic = message.Any(IsArabicCharacter);
            bool containsEnglish = message.Any(c => char.IsLetter(c) && c < 128);

            return language switch
            {
                LanguageEnum.ar => containsArabic && !containsEnglish, // Arabic only
                LanguageEnum.en => containsEnglish && !containsArabic, // English only
                _ => false
            };
        }
        private bool IsArabicCharacter(char c)
        {
            UnicodeCategory category = char.GetUnicodeCategory(c);
            return category == UnicodeCategory.OtherLetter;
        }


    }
}
