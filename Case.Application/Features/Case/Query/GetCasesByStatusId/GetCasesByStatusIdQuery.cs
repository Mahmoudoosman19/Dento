using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Query.GetCasesByStatusId
{
    public class GetCasesByStatusIdQuery : IQuery<IReadOnlyList<Domain.Entities.Case>>
    {
        public long StatusId { get; set; }
    }
}
