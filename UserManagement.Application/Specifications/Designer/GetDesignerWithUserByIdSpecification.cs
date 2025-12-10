using Common.Domain.Specification;

namespace UserManagement.Application.Specifications.Designer
{
    internal class GetDesignerWithUserByIdSpecification : Specification<Domain.Entities.User>
    {
        public GetDesignerWithUserByIdSpecification(Guid id,long roleId)
        {
            AddCriteria(c => c.RoleId == roleId);
            AddCriteria(x => x.Id == id || x.Id == id);

            //AddInclude($"{nameof(Domain.Entities.User)}");

        }
    }
}
