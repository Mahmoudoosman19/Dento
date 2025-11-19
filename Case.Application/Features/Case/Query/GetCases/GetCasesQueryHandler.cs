using Case.Application.Specifications;
using Case.Domain.Repositories;
using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Query.GetCases
{
    internal class GetCasesQueryHandler : IQueryHandler<GetCasesQuery, IEnumerable<GetCasesQueryResponse>>
    {
        private readonly ICaseUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetCasesQueryHandler(ICaseUnitOfWork uow,IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ResponseModel<IEnumerable<GetCasesQueryResponse>>> Handle(GetCasesQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetCasesSpecifications(request);
            (var cases,var count)= _uow.Repository<Domain.Entities.Case>().GetWithSpec(spec);

            var response = _mapper.Map<IEnumerable<GetCasesQueryResponse>>(cases);

            return await Task.FromResult(ResponseModel.Success(response, count));
        }
    }
}
