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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetDesignerQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public Task<ResponseModel<DesignerDto>> Handle(GetDesignerQuery request, CancellationToken cancellationToken)
        {
            var role = _uow.Repository<Domain.Entities.Role>().GetEntityWithSpec(new GetRoleByNameEnSpecification(Roles.Designer.ToString()));
            var designer = _uow.Repository<Domain.Entities.User>().GetEntityWithSpec(new GetDesignerWithUserByIdSpecification(request.Id, role!.Id));

            var designerDto = _mapper.Map<DesignerDto>(designer!);

            return Task.FromResult(ResponseModel.Success(designerDto));
        }
    }
}
