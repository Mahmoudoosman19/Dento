using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.DTOs;
using UserManagement.Application.Identity;
using UserManagement.Application.Specifications.Role;
using UserManagement.Application.Specifications.Supervisor;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Supervisor.Queries.GetSupervisorByNameAndStatusAndRoleId
{
    internal class GetSupervisorByNameAndStatusAndRoleIdQueryHandler : IQueryHandler<GetSupervisorByNameAndStatusAndRoleIdQuery, IReadOnlyList<UserDto>>
    {
        private readonly IGenericRepository<Domain.Entities.User> _supervisorRepo;
        private readonly IGenericRepository<Role> _roleRepo;
        private readonly IMapper _mapper;
        private readonly CustomUserManager _userManager;

        public GetSupervisorByNameAndStatusAndRoleIdQueryHandler(IMapper mapper, CustomUserManager userManager,
            IGenericRepository<Domain.Entities.User> supervisorRepo, IGenericRepository<Role> roleRepo)
        {
            _mapper = mapper;
            _userManager = userManager;
            _supervisorRepo = supervisorRepo;
            _roleRepo = roleRepo;
        }
        public Task<ResponseModel<IReadOnlyList<UserDto>>> Handle(GetSupervisorByNameAndStatusAndRoleIdQuery request,
            CancellationToken cancellationToken)
        {
            var role = _roleRepo.GetEntityWithSpec(new GetRoleByNameEnSpecification(Roles.Supervisor.ToString()));

            (var supervisor, int count) =
                _supervisorRepo.GetWithSpec(new GetSupervisorListQuerySpecification(request, role!.Id));

            var supervisors =
                _mapper.Map<IReadOnlyList<UserDto>>(supervisor!);

            return Task.FromResult(ResponseModel.Success(supervisors, count));
        }
    }
}
