using Common.Domain.Specification;
using UserManagement.Application.Features.Designer.Queries.GetListDesigners;

namespace UserManagement.Application.Specifications.Designer
{
    internal class FilterDesignersWithUserSpecification : Specification<Domain.Entities.Designer>
    {
        public FilterDesignersWithUserSpecification(GetListDesignersQuery request)
        {
            if (request.Name is not null)
                AddCriteria(c => c.User.FullNameEn!.Contains(request.Name)
                || c.User.FullNameAr!.Contains(request.Name)
                || c.User.UserName!.Contains(request.Name));

           

            if (request.Status is not null)
                AddCriteria(c => c.User.Status == request.Status);

            AddInclude(nameof(Domain.Entities.Designer.User));

            ApplyPaging(request.PageSize, request.PageIndex);
        }
    }
}

