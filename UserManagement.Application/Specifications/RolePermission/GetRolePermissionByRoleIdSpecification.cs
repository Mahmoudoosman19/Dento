using Common.Domain.Specification;

namespace UserManagement.Application.Specifications.RolePermission
{
    public class GetRolePermissionByRoleIdSpecification : Specification<Domain.Entities.RolePermission>
    {
        public GetRolePermissionByRoleIdSpecification(long roleId)
        {
            AddCriteria(x => x.RoleId == roleId);
            AddInclude($"{nameof(Domain.Entities.RolePermission.Permission)}");
        }
    }
}
