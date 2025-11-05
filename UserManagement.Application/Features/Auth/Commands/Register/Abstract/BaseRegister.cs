using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.Identity;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.Auth.Commands.Register.Abstract
{
    internal abstract class BaseRegister
    {
        protected readonly IMapper _mapper;
        protected readonly CustomUserManager _userManager;
        protected readonly IGenericRepository<Role> _roleRepo;

        public BaseRegister(
            IMapper mapper,
            CustomUserManager userManager,
            IGenericRepository<Role> roleRepo)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleRepo = roleRepo;
        }

        public abstract DesignerRegisterType Type { get; set; }

        public abstract Task<ResponseModel> Register(RegisterCommand registerDto);
    }
}
