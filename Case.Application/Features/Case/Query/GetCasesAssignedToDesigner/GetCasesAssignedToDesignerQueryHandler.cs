using Case.Application.Specifications;
using Case.Domain.Repositories;
using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Query.GetCasesAssignedToDesigner
{
    internal class GetCasesAssignedToDesignerQueryHandler : IQueryHandler<GetCasesAssignedToDesignerQuery, IEnumerable<GetCasesAssignedToDesignerResponse>>
    {
        private readonly ICaseUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetCasesAssignedToDesignerQueryHandler(ICaseUnitOfWork uow,IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ResponseModel<IEnumerable<GetCasesAssignedToDesignerResponse>>> Handle(GetCasesAssignedToDesignerQuery request, CancellationToken cancellationToken)
        {
            (var cases, int count) = _uow.Repository<Domain.Entities.Case>().GetWithSpec(new GetCasesAssignedToDesignerSpecifications(request));

            if (cases == null)
                return ResponseModel.Failure<IEnumerable<GetCasesAssignedToDesignerResponse>>("No Cases Assigned To This Designer Yet!");
                
            var response = _mapper.Map<IEnumerable<GetCasesAssignedToDesignerResponse>>(cases);

            return ResponseModel.Success(response,count);
        }
    }
}
