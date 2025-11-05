using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Resources;



namespace UserManagement.Application.Features.User.Commands.AddUser
{
    internal class AddUserCommandHandler : ICommandHandler<AddUserCommand>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Domain.Entities.User> _userRepo;
        private readonly ITokenExtractor _tokenExtractor;

        public AddUserCommandHandler(
            IGenericRepository<Domain.Entities.User> userRepo,
            IMapper mapper,
            ITokenExtractor tokenExtractor)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _tokenExtractor = tokenExtractor;
        }

        public async Task<ResponseModel> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenExtractor.GetUserId();

            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                return ResponseModel.Failure(Messages.UserNotFound);

            await AddUserProps(user, request);
            await _userRepo.SaveChangesAsync();

            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
        private async Task AddUserProps(Domain.Entities.User user, AddUserCommand request)
        {
            user.SetFullName(request.FullNameEn);
            user.SetBirthDate(request.BirthDate);
            user.SetGender(request.Gender);
        }
    }
}
