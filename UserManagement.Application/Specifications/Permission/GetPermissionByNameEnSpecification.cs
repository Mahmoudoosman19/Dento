using Common.Domain.Specification;

namespace UserManagement.Application.Specifications.Permission
{
    public class GetPermissionByNameEnSpecification : Specification<Domain.Entities.Permission>
    {
        public GetPermissionByNameEnSpecification(string nameEn)
        {
            AddCriteria(x => x.NameEn.Trim().ToUpper().Equals(nameEn.Trim().ToUpper()));
        }
    }
}
