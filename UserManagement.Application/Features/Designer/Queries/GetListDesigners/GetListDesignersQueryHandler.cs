using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.DTOs;
using UserManagement.Application.Specifications.Designer;
using UserManagement.Application.Specifications.Role;
using UserManagement.Application.Specifications.Supervisor;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Designer.Queries.GetListDesigners
{
    internal class GetListDesignersQueryHandler : IQueryHandler<GetListDesignersQuery, IReadOnlyList<UserDto>>
    {
        private readonly IGenericRepository<Domain.Entities.User> _designerRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Role> _roleRepo;


        public GetListDesignersQueryHandler(IGenericRepository<Domain.Entities.User> designerRepo, IMapper mapper, IGenericRepository<Role> roleRepo)
        {
            _designerRepo = designerRepo;
            _mapper = mapper;
            _roleRepo = roleRepo;
        }

        public Task<ResponseModel<IReadOnlyList<UserDto>>> Handle(GetListDesignersQuery request, CancellationToken cancellationToken)
        {
            var role = _roleRepo.GetEntityWithSpec(new GetRoleByNameEnSpecification(Roles.Designer.ToString()));

            (var designer, int count) =
                _designerRepo.GetWithSpec(new FilterDesignersWithUserSpecification(request, role!.Id));

            var designers =
                _mapper.Map<IReadOnlyList<UserDto>>(designer!);

            return Task.FromResult(ResponseModel.Success(designers, count));
        }
    }
}
