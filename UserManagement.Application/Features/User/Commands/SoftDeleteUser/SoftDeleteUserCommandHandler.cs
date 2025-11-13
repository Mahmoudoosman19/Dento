using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.User.Commands.SoftDeleteUser
{
    internal class SoftDeleteUserCommandHandler : ICommandHandler<SoftDeleteUserCommand>
    {
        private readonly IGenericRepository<Domain.Entities.User> _userRepo;

        public SoftDeleteUserCommandHandler(IGenericRepository<Domain.Entities.User> userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<ResponseModel> Handle(SoftDeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(request.userId);
            if(user == null) 
                return ResponseModel.Failure(Messages.UserNotFound);
            user.IsDeleted = true;

            await _userRepo.SaveChangesAsync();

            return ResponseModel.Success(Messages.UserDeletedSuccessfully);
        }
    }
}
