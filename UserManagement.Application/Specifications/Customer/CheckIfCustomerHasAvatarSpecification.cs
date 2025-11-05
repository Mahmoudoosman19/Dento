using Common.Domain.Specification;
namespace UserManagement.Application.Specifications.Customer
{
    internal class CheckIfCustomerHasAvatarSpecification : Specification<Domain.Entities.Customer>
    {
        public CheckIfCustomerHasAvatarSpecification(Guid userId)
        {
            AddCriteria(x => x.UserId == userId);
        }
    }
}
