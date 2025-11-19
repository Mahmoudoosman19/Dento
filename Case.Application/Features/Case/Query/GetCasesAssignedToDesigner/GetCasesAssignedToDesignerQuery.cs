using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Query.GetCasesAssignedToDesigner
{
    public class GetCasesAssignedToDesignerQuery  : IQuery<IEnumerable<GetCasesAssignedToDesignerResponse>>
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        public Guid DesignerId { get; set; }
    }
}
