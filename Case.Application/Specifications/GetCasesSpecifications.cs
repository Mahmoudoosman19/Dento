using Case.Application.Features.Case.Query.GetCases;
using Case.Application.Features.Case.Query.GetCasesByStatusId;
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
            if (request.DesignerId != null)
                AddCriteria(c => c.DesignertId == request.DesignerId);
            
            if (request.CustomerId != null)
                AddCriteria(c => c.CustomerId == request.CustomerId);
        }
        public GetCasesSpecifications(GetCasesByStatusIdQuery request)
        {
            AddCriteria(c => c.StatusId == request.StatusId);
        }
    }
}
