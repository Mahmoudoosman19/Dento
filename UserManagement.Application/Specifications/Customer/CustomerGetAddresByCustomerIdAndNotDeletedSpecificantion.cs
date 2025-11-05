using Common.Domain.Specification;
using UserManagement.Domain.Entites;

namespace UserManagement.Application.Specifications.Customer
{
    public class ListCustomerAddressSpecification : Specification<Address>
    {
        public ListCustomerAddressSpecification(Guid UserId) 
        {
            AddCriteria(c=>c.UserId == UserId&&!c.IsDeleted);
        }
    }
}
