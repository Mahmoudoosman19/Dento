using Case.Domain.Repositories;
using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
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
        private readonly ITokenExtractor _tokenExtractor;

        public GetCaseByIdQueryHandler(ICaseUnitOfWork uow,IMapper mapper, ITokenExtractor tokenExtractor)
        {
            _uow = uow;
            _mapper = mapper;
            _tokenExtractor = tokenExtractor;
        }

        public async Task<ResponseModel<GetCaseByIdQueryResponse>> Handle(GetCaseByIdQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _tokenExtractor.GetUserId();
            var currentUserRole = _tokenExtractor.GetUserRole();

            var casee = await _uow.Repository<Domain.Entities.Case>().GetByIdAsync(request.Id);

            if (casee == null) 
                return ResponseModel.Failure<GetCaseByIdQueryResponse>("Case Not Exist!");
            if (currentUserRole == "Designer" && casee.DesignertId != currentUserId)
                return ResponseModel.Failure<GetCaseByIdQueryResponse>("Unauthorized");
            var response = _mapper.Map<GetCaseByIdQueryResponse>(casee);

            return await Task.FromResult(ResponseModel.Success(response, 1));
        }
    }
}
