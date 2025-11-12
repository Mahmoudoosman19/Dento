using Common.Domain.Specification;

namespace UserManagement.Application.Specifications.Designer
{
    public class GetDesignerByUserEmailSpecification : Specification<Domain.Entities.Designer>
    {
        public GetDesignerByUserEmailSpecification(string userEmail)
        {
            AddCriteria(x => x.User.NormalizedEmail == userEmail.ToUpper());
        }
    }
}
