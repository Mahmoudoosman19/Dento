using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using MapsterMapper;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.User.Commands.UpdateUserProfile
{
    internal class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Domain.Entities.User> _userRepo;
        private readonly ITokenExtractor _tokenExtractor;

        public UpdateProfileCommandHandler(
            IGenericRepository<Domain.Entities.User> userRepo,
            IMapper mapper,
            ITokenExtractor tokenExtractor)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _tokenExtractor = tokenExtractor;
        }

        public async Task<ResponseModel> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenExtractor.GetUserId();

            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                return ResponseModel.Failure(Messages.UserNotFound);

            await UpdateProfileProps(user, request);
            await _userRepo.SaveChangesAsync();

            return ResponseModel.Success(Messages.SuccessfulOperation);
        }

        private async Task UpdateProfileProps(Domain.Entities.User user, UpdateProfileCommand request)
        {
            user.SetFullName(request.FullNameEn);
            user.SetBirthDate(request.BirthDate);
            user.SetPhoneNumber(request.PhoneNumber);
            user.SetGender(request.Gender);
        }
    }
}
