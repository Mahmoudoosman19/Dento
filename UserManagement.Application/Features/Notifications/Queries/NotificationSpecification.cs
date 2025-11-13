using Common.Domain.Specification;

namespace UserManagement.Application.Features.Notifications.Queries
{
    public class NotificationSpecification : Specification<Domain.Entities.Notifications>
    {
        public NotificationSpecification(GetNotificationsByUserIdQuery request, string userRole)
        {
            ApplyPaging(request.PageSize, request.PageIndex);
            if (userRole != "Admin")
                AddCriteria(n => n.UserId == request.UserId);

            AddOrderByDescending(n => n.CreatedOnUtc);
        }
    }
}
