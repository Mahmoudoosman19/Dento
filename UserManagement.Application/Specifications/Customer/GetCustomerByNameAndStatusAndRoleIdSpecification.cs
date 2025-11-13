using Common.Domain.Specification;
using UserManagement.Application.Features.Customer.Queries.GetCustomerByRoleId;
//using UserManagement.Application.Features.Customer.Queries.GetCustomerByRoleId;

namespace UserManagement.Application.Specifications.Customer
{
    internal class GetCustomerByNameAndStatusAndRoleIdSpecification : Specification<Domain.Entities.User>
    {
        public GetCustomerByNameAndStatusAndRoleIdSpecification(GetCustomerByRoleIdQuery request, long roleId)
        {
            ApplyPaging(request.PageSize, request.PageIndex);
            if (request.Name is not null)
                AddCriteria(c => c.FullNameAr!.Contains(request.Name)
                || c.FullNameEn!.Contains(request.Name)
                || c.UserName!.Contains(request.Name));

            if (request.Status is not null)
                AddCriteria(c => c.Status == request.Status);

            AddCriteria(c => c.RoleId == roleId);
        }
    }
}
