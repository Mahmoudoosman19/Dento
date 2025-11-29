using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.DTOs;
using UserManagement.Application.Specifications.Designer;
using UserManagement.Application.Specifications.Role;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Designer.Queries.GetDesignerById
{
    internal class GetDesignerQueryHandler : IQueryHandler<GetDesignerQuery, DesignerDto>
    {
        private readonly IGenericRepository<Domain.Entities.User> _userRepo;
        private readonly IGenericRepository<Domain.Entities.Role> _roleRepo;
        private readonly IMapper _mapper;

        public GetDesignerQueryHandler(IGenericRepository<Domain.Entities.User> userRepo,IGenericRepository<Role> roleRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _roleRepo = roleRepo;
        }
        public Task<ResponseModel<DesignerDto>> Handle(GetDesignerQuery request, CancellationToken cancellationToken)
        {
            var role = _roleRepo.GetEntityWithSpec(new GetRoleByNameEnSpecification(Roles.Designer.ToString()));
            var designer = _userRepo.GetEntityWithSpec(new GetDesignerWithUserByIdSpecification(request.Id,role!.Id));

            var designerDto = _mapper.Map<DesignerDto>(designer!);

            return Task.FromResult(ResponseModel.Success(designerDto));
        }
    }
}
