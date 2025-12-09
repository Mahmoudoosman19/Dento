using Case.Application.Features.Case.Query.GetCases;
using Case.Application.Specifications;
using Case.Domain.Entities;
using Case.Domain.Repositories;
using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Query.GetCasesByStatusId
{
    internal class GetCasesByStatusIdQueryHandler : IQueryHandler<GetCasesByStatusIdQuery, IReadOnlyList<GetCasesByStatusIdQueryResponse>>
    {
        private readonly ICaseUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetCasesByStatusIdQueryHandler(ICaseUnitOfWork uow,IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ResponseModel<IReadOnlyList<GetCasesByStatusIdQueryResponse>>> Handle(GetCasesByStatusIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetCasesSpecifications(request);
            (var cases, var count) = _uow.Repository<Domain.Entities.Case>().GetWithSpec(spec);
            var response = _mapper.Map<IReadOnlyList<GetCasesByStatusIdQueryResponse>>(cases);

            return await Task.FromResult(ResponseModel.Success(response, count));
        }
    }
}
