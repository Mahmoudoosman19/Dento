using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
using UserManagement.Application.Identity;
using UserManagement.Application.Specifications.Role;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.Register.Type
{
    internal class SupervisorRegisterType : BaseRegister
    {
        public SupervisorRegisterType(
            IMapper mapper,
            CustomUserManager userManager,
            IGenericRepository<Role> roleRepo)
            : base(
                mapper,
                userManager,
                roleRepo)
        { }

        public override RegisterType Type { get; set; } = RegisterType.Supervisor;

        public async override Task<ResponseModel> Register(RegisterCommand registerDto)
        {
            var user = _mapper.Map<Domain.Entities.User>(registerDto.Supervisor!);
            user.SetStatus(UserStatus.Active);
            var adminRole = _roleRepo.GetEntityWithSpec(new GetRoleByNameEnSpecification(Roles.SuperVisor.ToString()));
            user.AssignRole(adminRole!.Id);
            user.ConfirmEmail();
            user.ConfirmPhoneNumber();
            var result = await _userManager.CreateAsync(user, registerDto.Supervisor!.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine,
                    result.Errors.Select(e => e.Description));

                return ResponseModel.Failure(errors);
            }

            return ResponseModel.Success();
        }
    }
}
