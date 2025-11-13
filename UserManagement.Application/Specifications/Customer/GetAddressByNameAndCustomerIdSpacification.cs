using Common.Domain.Specification;
using UserManagement.Application.Features.Customer.Commands.CustomerAddAddress;

//using UserManagement.Application.Features.Customer.Commands.CustomerAddAddress;
using UserManagement.Domain.Entites;

namespace UserManagement.Application.Specifications.Customer
{
    public class AddAddressCustomerSpecification : Specification<Address>
    {
        public AddAddressCustomerSpecification(AddAddressCommand request, Guid userId)
        {
            if (request.AddressName is not null)
                AddCriteria(c => c.AddressName == request.AddressName && c.UserId == userId);
        }
    }
}
