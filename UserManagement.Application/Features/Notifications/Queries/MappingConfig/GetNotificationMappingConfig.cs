using Common.Application.Localization;
using Mapster;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Features.Notifications.Queries.MappingConfig
{
    public class GetNotificationMappingConfig : IRegister
    {
        private ILocalizer? _localizer;

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.Notifications, NotificationDto>()
                 .Map(des => des.IsSeen, src => src.IsSeen)
                 .Map(des => des.Content, src => src.Content)
                 .Map(des => des.CreatedOnUtc, src => src.CreatedOnUtc)
                 .Map(des => des.Title, src => src.Title)
                 .Map(des => des.ModifiedOnUtc, src => src.ModifiedOnUtc)
                 .Map(des => des.UserId, src => src.UserId);
        }
    }
}