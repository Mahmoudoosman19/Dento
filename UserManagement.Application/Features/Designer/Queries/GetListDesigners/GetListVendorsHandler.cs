using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.DTOs;
using UserManagement.Application.Specifications.Designer;

namespace UserManagement.Application.Features.Designer.Queries.GetListDesigners
{
    internal class GetListVendorsHandler : IQueryHandler<GetListDesignersQuery, IReadOnlyList<DesignerDto>>
    {
        private readonly IGenericRepository<Domain.Entities.Designer> _designerRepo;
        private readonly IMapper _mapper;


        public GetListVendorsHandler(IGenericRepository<Domain.Entities.Designer> vendorRepo, IMapper mapper)
        {
            _designerRepo = vendorRepo;
            _mapper = mapper;
        }
        public Task<ResponseModel<IReadOnlyList<DesignerDto>>> Handle(GetListDesignersQuery request, CancellationToken cancellationToken)
        {
            (var designers, int count) = _designerRepo.GetWithSpec(new FilterDesignersWithUserSpecification(request));

            var designersDto = _mapper.Map<IReadOnlyList<DesignerDto>>(designers);

            return Task.FromResult(ResponseModel.Success(designersDto, count));
        }
    }
}
