using Case.Application.Specifications;
using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Query.GetCasesByStatusId
{
    internal class GetCasesByStatusIdQueryHandler : IQueryHandler<GetCasesByStatusIdQuery>
    {
        public Task<ResponseModel> Handle(GetCasesByStatusIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetCasesSpecifications(request);

            throw new NotImplementedException();
        }
    }
}
