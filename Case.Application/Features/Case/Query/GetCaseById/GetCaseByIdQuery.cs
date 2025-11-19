using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Query.GetCaseById
{
    public class GetCaseByIdQuery : IQuery<GetCaseByIdQueryResponse>
    {
        public Guid Id { get; set; }

    }
}
