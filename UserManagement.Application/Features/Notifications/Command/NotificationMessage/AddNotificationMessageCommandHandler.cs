using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using UserManagement.Application.Features.Notifications.Command.NotificationMessage;
using UserManagement.Application.Specifications.MessageNotification;
using UserManagement.Domain.Enums;

public class AddNotificationMessageCommandHandler : ICommandHandler<AddNotificationMessageCommand>
{
    private readonly IGenericRepository<NotificationMessages> _notificationRepo;
    public AddNotificationMessageCommandHandler(
        IGenericRepository<NotificationMessages> notificationRepo)
    {
        _notificationRepo = notificationRepo;
    }

    public async Task<ResponseModel> Handle(AddNotificationMessageCommand request, CancellationToken cancellationToken)
    {

        var data = _notificationRepo.GetEntityWithSpec(new GetMessageNotificationByMessageEnumKeySpecification(request.ResourceKey));
        if (data != null)
            return ResponseModel.Failure("NotificationMessageAlreadyExist");
        var notificationMessages = new List<NotificationMessages>();
        
        var arabicMessage = new NotificationMessages();
            arabicMessage.CreateMessage(request.ResourceKey, request.ResourceValueArbice, LanguageEnum.ar);
            notificationMessages.Add(arabicMessage);
        
        var englishMessage = new NotificationMessages();
            englishMessage.CreateMessage(request.ResourceKey, request.ResourceValueEnglish, LanguageEnum.en);
            notificationMessages.Add(englishMessage);

        await _notificationRepo.AddRangeAsync(notificationMessages);
        await _notificationRepo.SaveChangesAsync();

        return ResponseModel.Success(Messages.SuccessfulOperation);
    }
   

}
