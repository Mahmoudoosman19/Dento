using Common.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Entities
{
    public class NotificationMessages: Entity<Guid>
    {
        public MessageEnumKey ResourceKey { get; set; }    
        public string ResourceValue { get; set; }    
        public LanguageEnum CurrentLanguage { get; set; }

       
        public void CreateMessage(MessageEnumKey resourceKey, string resourceValue, LanguageEnum currentLanguage)
        {
            ResourceKey = resourceKey;
            ResourceValue = resourceValue;
            CurrentLanguage = currentLanguage;
        }
        public string GetFormattedMessage(params object[] args)
        {
            return string.Format(ResourceValue, args); 
        }
    }
}
