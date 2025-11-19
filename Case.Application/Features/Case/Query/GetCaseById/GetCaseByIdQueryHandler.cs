using Case.Domain.Repositories;
using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Query.GetCaseById
{
    internal class GetCaseByIdQueryHandler : IQueryHandler<GetCaseByIdQuery,GetCaseByIdQueryResponse>
    {
        private readonly ICaseUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetCaseByIdQueryHandler(ICaseUnitOfWork uow,IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ResponseModel<GetCaseByIdQueryResponse>> Handle(GetCaseByIdQuery request, CancellationToken cancellationToken)
        {
            var casee = await _uow.Repository<Domain.Entities.Case>().GetByIdAsync(request.Id);
            if (casee == null)
                return ResponseModel.Failure<GetCaseByIdQueryResponse>("Case Not Exist!");

            var response = _mapper.Map<GetCaseByIdQueryResponse>(casee);

            return await Task.FromResult(ResponseModel.Success(response, 1));
        }
    }
}
