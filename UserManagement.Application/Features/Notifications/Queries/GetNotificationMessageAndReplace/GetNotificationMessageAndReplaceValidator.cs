using Common.Application.Extensions.String;
using System.Collections.Generic;
using FluentValidation;
using UserManagement.Application.Features.Notifications.Command.NotificationMessage;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Notifications.Queries.GetNotificationMessageAndReplace
{
    public class GetNotificationMessageAndReplaceValidator : AbstractValidator<GetNotificationMessageAndReplaceQuery>
    {
        public GetNotificationMessageAndReplaceValidator()
        {
            RuleFor(x => x.ReplaceValues)
                .NotEmpty().WithMessage(Messages.EmptyField) 
                .Must((query, replaceValues) => BeValidLanguageReplacement(replaceValues, query))
                .WithMessage("InvalidLanguage"); 
        }

        private bool BeValidLanguageReplacement
            (IEnumerable<string> replaceValues, GetNotificationMessageAndReplaceQuery query)
        {
            var language = query.language; 

            if (language == LanguageEnum.en)
            {
                return !replaceValues.Any(value => value.IsArabicLanguage());
            }
            else if (language == LanguageEnum.ar)
            {
                return !replaceValues.Any(value => value.IsEnglishLanguage());
            }

            return true; 
        }
    }
}
