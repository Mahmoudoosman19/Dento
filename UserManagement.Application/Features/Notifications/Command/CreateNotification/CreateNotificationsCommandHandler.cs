using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;

namespace UserManagement.Application.Features.Notifications.Command.CreateNotification
{
    internal class CreateNotificationsCommandHandler : ICommandHandler<CreateNotificationsCommand>
    {
        private readonly IGenericRepository<Domain.Entities.Notifications> _notificationsRepository;
        private readonly IMapper _mapper;
        public CreateNotificationsCommandHandler(IGenericRepository<Domain.Entities.Notifications> notificationsRepository, IMapper mapper)
        {
            _notificationsRepository = notificationsRepository;
            _mapper = mapper;
        }
        public async Task<ResponseModel> Handle(CreateNotificationsCommand request, CancellationToken cancellationToken)
        {
            var notificationsMapped = _mapper.Map<Domain.Entities.Notifications>(request);
            await _notificationsRepository.AddAsync(notificationsMapped);
            await _notificationsRepository.SaveChangesAsync(cancellationToken);
            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
