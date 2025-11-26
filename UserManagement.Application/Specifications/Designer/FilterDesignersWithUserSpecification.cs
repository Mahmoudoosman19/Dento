using Common.Domain.Specification;
using UserManagement.Application.Features.Designer.Queries.GetListDesigners;

namespace UserManagement.Application.Specifications.Designer
{
    internal class FilterDesignersWithUserSpecification : Specification<Domain.Entities.User>
    {
        public FilterDesignersWithUserSpecification(GetListDesignersQuery request, long roleId)
        {
            if (request.Name is not null)
                AddCriteria(c => c.FullNameEn!.Contains(request.Name)
                || c.FullNameAr!.Contains(request.Name)
                || c.UserName!.Contains(request.Name));

           

            if (request.Status is not null)
                AddCriteria(c => c.Status == request.Status);


            ApplyPaging(request.PageSize, request.PageIndex);
            AddCriteria(c => c.RoleId == roleId);
        }
    }
}

