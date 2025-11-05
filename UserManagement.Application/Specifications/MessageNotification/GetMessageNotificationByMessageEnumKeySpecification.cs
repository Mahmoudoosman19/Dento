using Common.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Specifications.MessageNotification
{
    public class GetMessageNotificationByMessageEnumKeySpecification : Specification<Domain.Entities.NotificationMessages>
    {
        public GetMessageNotificationByMessageEnumKeySpecification(MessageEnumKey messageEnumKey)
        {
            AddCriteria(x => x.ResourceKey == messageEnumKey);
        }
    }
   
}
