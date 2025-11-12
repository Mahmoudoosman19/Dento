using Common.Domain.Specification;

namespace UserManagement.Application.Specifications.Designer
{
    internal class GetDesignerWithUserByIdSpecification : Specification<Domain.Entities.Designer>
    {
        public GetDesignerWithUserByIdSpecification(Guid id)
        {
            AddCriteria(x => x.Id == id || x.User.Id == id);

            AddInclude($"{nameof(Domain.Entities.Designer.User)}");
        }
    }
}
