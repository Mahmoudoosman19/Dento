using Common.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Specifications.MessageNotification
{
    public class GetMessageNotificationByMessageEnumKeyAndLanguageEnumSpecification
        :Specification<Domain.Entities.NotificationMessages>

    {
        public GetMessageNotificationByMessageEnumKeyAndLanguageEnumSpecification(MessageEnumKey messageEnumKey,LanguageEnum languageEnum)
        {
            AddCriteria(x => x.ResourceKey == messageEnumKey && x.CurrentLanguage == languageEnum);
        }
    }
}
