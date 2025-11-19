using Case.Application.Features.Case.Query.GetCases;
using Common.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Specifications
{
    public class GetCasesSpecifications : Specification<Domain.Entities.Case>
    {
        public GetCasesSpecifications(GetCasesQuery request)
        {
            ApplyPaging(request.PageSize, request.PageIndex);
        }
    }
}
