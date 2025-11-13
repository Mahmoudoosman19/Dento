using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Notifications.Command.NotificationMessage
{
    public class AddNotificationMessageCommand:ICommand
    {
        public MessageEnumKey ResourceKey { get; set; }
        public string ResourceValueArbice { get; set; }
        public string ResourceValueEnglish { get; set; }
    }
}
