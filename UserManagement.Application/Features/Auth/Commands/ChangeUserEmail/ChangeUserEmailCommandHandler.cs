using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using UserManagement.Application.Identity;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Auth.Commands.ChangeUserEmail
{
    internal class ChangeUserEmailCommandHandler : ICommandHandler<ChangeUserEmailCommand>
    {
        private readonly ITokenExtractor _tokenExtractor;
        private readonly IGenericRepository<Domain.Entities.User> _userRepo;
        private readonly CustomUserManager _userManager;
        public ChangeUserEmailCommandHandler(ITokenExtractor tokenExtractor, IGenericRepository<Domain.Entities.User> userRepo, CustomUserManager userManager)
        {
            _tokenExtractor = tokenExtractor;
            _userRepo = userRepo;
            _userManager = userManager;
        }
        public async Task<ResponseModel> Handle(ChangeUserEmailCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByIdAsync(_tokenExtractor.GetUserId().ToString());
            if (user == null)
                return ResponseModel.Failure(Messages.FailedToFetchUserData);

            var existingUser = await _userRepo.GetByIdAsync(_tokenExtractor.GetUserId());
            if (existingUser == null)
                return ResponseModel.Failure(Messages.FailedToFetchUserData);

            user.Email = request.Email;
            await _userManager.UpdateAsync(user);

            existingUser.Email = request.Email;
            _userRepo.Update(existingUser);
            await _userRepo.SaveChangesAsync();

            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
