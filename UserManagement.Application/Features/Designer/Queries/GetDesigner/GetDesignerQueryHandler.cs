using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.DTOs;
using UserManagement.Application.Specifications.Designer;

namespace UserManagement.Application.Features.Designer.Queries.GetDesignerById
{
    internal class GetDesignerQueryHandler : IQueryHandler<GetDesignerQuery, DesignerDto>
    {
        private readonly IGenericRepository<Domain.Entities.Designer> _designerRepo;
        private readonly IMapper _mapper;

        public GetDesignerQueryHandler(IGenericRepository<Domain.Entities.Designer> vendorRepo, IMapper mapper)
        {
            _designerRepo = vendorRepo;
            _mapper = mapper;
        }
        public Task<ResponseModel<DesignerDto>> Handle(GetDesignerQuery request, CancellationToken cancellationToken)
        {
            var designer = _designerRepo.GetEntityWithSpec(new GetDesignerWithUserByIdSpecification(request.Id));

            var designerDto = _mapper.Map<DesignerDto>(designer!);

            return Task.FromResult(ResponseModel.Success(designerDto));
        }
    }
}
