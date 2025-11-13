using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Notifications.Queries.GetNotificationMessageAndReplace
{
    public class GetNotificationMessageAndReplaceQuery
        :IQuery<GetNotificationMessageQueryResponse>
    {
        public MessageEnumKey MessageKey { get; set; }
        public   LanguageEnum language { get; set; }    
        public string[] ReplaceValues { get; set; }
    }
}
