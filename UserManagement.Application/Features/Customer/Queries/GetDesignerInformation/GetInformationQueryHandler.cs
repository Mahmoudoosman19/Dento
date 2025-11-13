using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.Specifications.Designer;

namespace UserManagement.Application.Features.Customer.Queries.GetVendorInformation
{
    public class GetInformationQueryHandler : IQueryHandler<GetInformationQuery, GetInformationResponse>
    {
        private readonly IGenericRepository<UserManagement.Domain.Entities.Designer> _vendorRepo;
        private readonly IMapper _mapper;
        public GetInformationQueryHandler( IGenericRepository<Domain.Entities.Designer> vendorRepo, IMapper mapper)
        {
            _vendorRepo = vendorRepo;
            _mapper = mapper;
        }
        public async Task<ResponseModel<GetInformationResponse>> Handle(GetInformationQuery request, CancellationToken cancellationToken)
        {
            
            var data =  _vendorRepo.GetEntityWithSpec(new GetDesignerByUserIdWithUserSpecification(request.VendorId));
            if (data == null)
            {
                return ResponseModel.Failure<GetInformationResponse>(Messages.Vendordetailsnotfoundinvendorrepository);
            }

            var mapping = _mapper.Map<GetInformationResponse>(data);

            return ResponseModel.Success<GetInformationResponse>(mapping);

        }
    }
}
