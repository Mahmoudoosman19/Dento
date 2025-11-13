using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using MapsterMapper;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Features.Notifications.Queries
{
    public class GetNotificationsByUserIdQueryHandler : IQueryHandler<GetNotificationsByUserIdQuery, List<NotificationDto>>
    {
        private readonly IGenericRepository<Domain.Entities.Notifications> _notificationRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenExtractor _tokenExtractor;

        public GetNotificationsByUserIdQueryHandler(
           IGenericRepository<Domain.Entities.Notifications> notificationRepo,
           IMapper mapper,
           IUnitOfWork unitOfWork,
           ITokenExtractor itokenextractor)

        {
            _mapper = mapper;
            _notificationRepo = notificationRepo;
            _unitOfWork = unitOfWork;
            _tokenExtractor = itokenextractor;
        }

        public async Task<ResponseModel<List<NotificationDto>>> Handle(GetNotificationsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<Domain.Entities.User>().GetByIdAsync(request.UserId);

            if (user == null)
                return ResponseModel.Failure<List<NotificationDto>>(Messages.UserNotFound);

            var userRole = _tokenExtractor.GetUserRole();

            var spec = new NotificationSpecification(request, userRole);
            (var notifications, int count) = _notificationRepo.GetWithSpec(spec);

            if (!notifications.Any())
                return ResponseModel.Failure<List<NotificationDto>>(Messages.NotFound);

            var mappedNotification = _mapper.Map<List<NotificationDto>>(notifications);

            return ResponseModel.Success(mappedNotification, count);
        }
    }
}
