using Common.Domain.Specification;

namespace UserManagement.Application.Specifications.User
{
    public class DashboardGetUserByIdWithRoleSpecification : Specification<Domain.Entities.User>
    {
        public DashboardGetUserByIdWithRoleSpecification(Guid Id)
        {
            AddCriteria(c=>c.Id == Id); 
            AddInclude(nameof(UserManagement.Domain.Entities.User.Role));
        }
    }
}
