using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IGenericRepository<Domain.Entities.User> _userRepository;
        public DeleteUserCommandHandler(IGenericRepository<Domain.Entities.User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResponseModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userId, cancellationToken);

            if (user == null)
                return ResponseModel.Failure(Messages.UserNotFound);

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();

            return ResponseModel.Success(Messages.UserDeletedSuccessfully);
        }
    }
}
