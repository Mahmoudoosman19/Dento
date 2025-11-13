using Common.Application.Abstractions.Messaging;
using System.ComponentModel;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Features.Notifications.Queries
{
    public class GetNotificationsByUserIdQuery : IQuery<List<NotificationDto>>
    {
        public Guid Id { get; set; }
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        [DisplayName("رقم التعريف")]
        public Guid UserId { get; set; }
    }
}
