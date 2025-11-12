using Common.Domain.Specification;

namespace UserManagement.Application.Specifications.Designer
{
    public class GetDesignerByUserIdWithUserSpecification : Specification<Domain.Entities.Designer>
    {
        public GetDesignerByUserIdWithUserSpecification(Guid userId)
        {
            AddCriteria(x => x.UserId == userId);
            AddInclude("User");
        }
    }
}
