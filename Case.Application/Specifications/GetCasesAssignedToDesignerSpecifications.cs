using Case.Application.Features.Case.Query.GetCasesAssignedToDesigner;
using Common.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Specifications
{
    internal class GetCasesAssignedToDesignerSpecifications : Specification<Domain.Entities.Case>
    {
        public GetCasesAssignedToDesignerSpecifications(GetCasesAssignedToDesignerQuery request)
        {
            ApplyPaging(request.PageSize, request.PageIndex);
            AddCriteria(c=>c.DesignertId == request.DesignerId);
        }
    }
}
