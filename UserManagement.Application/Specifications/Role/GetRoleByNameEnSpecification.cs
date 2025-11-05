using Common.Domain.Specification;
namespace UserManagement.Application.Specifications.Role
{
    public class GetRoleByNameEnSpecification : Specification<Domain.Entities.Role>
    {
        public GetRoleByNameEnSpecification(string nameEn)
        {
            AddCriteria(x => x.NameEn.Trim().ToUpper().Equals(nameEn.Trim().ToUpper()));
        }
    }
}
