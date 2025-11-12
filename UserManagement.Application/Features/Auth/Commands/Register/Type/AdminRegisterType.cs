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
    internal class AdminRegisterType : BaseRegister
    {
        public AdminRegisterType(
            IMapper mapper,
            CustomUserManager userManager,
            IGenericRepository<Role> roleRepo)
            : base(
                  mapper,
                  userManager,
                  roleRepo)
        { }

        public override RegisterType Type { get; set; } = RegisterType.Admin;

        public override async Task<ResponseModel> Register(RegisterCommand registerDto)
        {
            var user = _mapper.Map<Domain.Entities.User>(registerDto.Admin!);
            user.SetStatus(UserStatus.Active);
            user.ConfirmEmail();
            user.ConfirmPhoneNumber();
            var adminRole = _roleRepo.GetEntityWithSpec(new GetRoleByNameEnSpecification(Roles.Admin.ToString()));
            user.AssignRole(adminRole!.Id);

            var result = await _userManager.CreateAsync(user, registerDto.Admin!.Password);
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
