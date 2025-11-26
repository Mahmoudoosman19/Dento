using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.User.Commands.UpdateUser
{
    internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, ResponseModel>
    {
        private readonly CustomUserManager _userManager;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(CustomUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ResponseModel>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
     
            var user = await _userManager.GetUserWithRoleAsync(request.Id);
            if (user == null)
                return ResponseModel.Failure("Supervisor not found");

        
            user.UserName = request.UserName.Replace(" ", "");
            user.SetFullName(request.FullNameEn, request.FullNameAr);
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.SetBirthDate(request.BirthDate);
            user.SetGender(request.Gender);


            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return ResponseModel.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));

            return ResponseModel.Success();
        }
    }
}
