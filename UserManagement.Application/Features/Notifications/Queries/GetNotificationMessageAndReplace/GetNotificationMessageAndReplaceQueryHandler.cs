using Common.Application.Abstractions.Messaging;
using Common.Application.Extensions.String;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Specifications.MessageNotification;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Notifications.Queries.GetNotificationMessageAndReplace
{
    public class GetNotificationMessageAndReplaceQueryHandler
        : IQueryHandler<GetNotificationMessageAndReplaceQuery, GetNotificationMessageQueryResponse>
    {
        private readonly IGenericRepository<NotificationMessages> _messageRepo;

        public GetNotificationMessageAndReplaceQueryHandler(IGenericRepository<NotificationMessages> messageRepo)
        {
            _messageRepo = messageRepo;
        }

        public async Task<ResponseModel<GetNotificationMessageQueryResponse>> Handle(GetNotificationMessageAndReplaceQuery request, CancellationToken cancellationToken)
        {
       
            var formattedMessage = await GetFormattedNotification(request.MessageKey, request.language,request.ReplaceValues);
            var response = new GetNotificationMessageQueryResponse { Message = formattedMessage };
            return ResponseModel.Success(response);
        }

        private async Task<string> GetFormattedNotification(MessageEnumKey key, LanguageEnum language, params object[] args)
        {
            var notificationMessage = _messageRepo.GetEntityWithSpec(
                new GetMessageNotificationByMessageEnumKeyAndLanguageEnumSpecification(key, language));

            if (notificationMessage == null)
                return "Message not found";

            return notificationMessage.GetFormattedMessage(args);
        }
    }
}
