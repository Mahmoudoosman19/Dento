using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.Notifications.Command.CreateNotification
{
    public class CreateNotificationsCommand : ICommand
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Roles { get; set; }    
        public Guid UserId { get; set; }
    }
}
