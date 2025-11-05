using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.User.Commands.RestoredUser
{
    public class RestoredUserCommandHandler:ICommandHandler<RestoredUserCommand>
    {
        private readonly IGenericRepository<Domain.Entities.User> _userRepository;
        public RestoredUserCommandHandler(IGenericRepository<Domain.Entities.User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResponseModel> Handle(RestoredUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userId, cancellationToken);
            if (user == null)
                return ResponseModel.Failure(Messages.UserNotFound);

            user.Restore();
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return ResponseModel.Success(Messages.UserRestoredSuccessfully);
        }
    }

}
