using Case.Application.Specifications;
using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Query.GetCasesByStatusId
{
    internal class GetCasesByStatusIdQueryHandler : IQueryHandler<GetCasesByStatusIdQuery, IReadOnlyList<Domain.Entities.Case>>
    {
        

        Task<ResponseModel<IReadOnlyList<Domain.Entities.Case>>> IRequestHandler<GetCasesByStatusIdQuery, ResponseModel<IReadOnlyList<Domain.Entities.Case>>>.Handle(GetCasesByStatusIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
