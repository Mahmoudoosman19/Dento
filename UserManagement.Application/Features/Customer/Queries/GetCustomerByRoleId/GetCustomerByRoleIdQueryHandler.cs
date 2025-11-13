using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.DTOs;
using UserManagement.Application.Specifications.Customer;
using UserManagement.Application.Specifications.Role;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Customer.Queries.GetCustomerByRoleId
{
    public class GetCustomerByRoleIdQueryHandler : IQueryHandler<GetCustomerByRoleIdQuery, IReadOnlyList<UserDto>>
    {
        private readonly IGenericRepository<Domain.Entities.User> _userRepo;
        private readonly IGenericRepository<Role> _roleRepo;
        private readonly IMapper _mapper;
        private readonly CustomUserManager _userManager;

        public GetCustomerByRoleIdQueryHandler(IMapper mapper, CustomUserManager userManager,
            IGenericRepository<Domain.Entities.User> userRepo, IGenericRepository<Role> roleRepo)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }
        public Task<ResponseModel<IReadOnlyList<UserDto>>> Handle(GetCustomerByRoleIdQuery request,
            CancellationToken cancellationToken)
        {
            var role = _roleRepo.GetEntityWithSpec(new GetRoleByNameEnSpecification(Roles.Customer.ToString()));

            (var customer, int count) = _userRepo.GetWithSpec(new GetCustomerByNameAndStatusAndRoleIdSpecification(request, role!.Id));

            var customers = _mapper.Map<IReadOnlyList<UserDto>>(customer!);

            return Task.FromResult(ResponseModel.Success(customers, count));
        }
    }
}

