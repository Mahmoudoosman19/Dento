using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.DTOs;
using UserManagement.Application.Features.Designer.Queries.GetTopDesigners;

namespace UserManagement.Application.Features.Designer.Queries.GetTopVendors
{
    public class GetTopDesignersQueryHandler : IQueryHandler<GetTopDesignersQuery, GetTopDesignerResponse>
    {
        private readonly IGenericRepository<Domain.Entities.Designer> _designerRepo;
        private readonly IMapper _mapper;

        public GetTopDesignersQueryHandler(
            IGenericRepository<Domain.Entities.Designer> vendorRepo,
            IMapper mapper)
        {
            _designerRepo = vendorRepo;
            _mapper = mapper;
        }

        public async Task<ResponseModel<GetTopDesignerResponse>> Handle(GetTopDesignersQuery request, CancellationToken cancellationToken)
        {
            var allVendors = _designerRepo.Get();

            int totalVendorsCount = allVendors.Count();

            var topVendors = allVendors
                .Take(5)
                .Select(vendor => new VendorCountDto
                {
                   
                })
                .ToList();

            var response = new GetTopDesignerResponse
            {
                TotalDesignersCount = totalVendorsCount,
                TopDesigners = topVendors
            };

            return ResponseModel.Success(response);
        }
    }
}

