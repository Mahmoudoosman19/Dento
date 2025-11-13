using Common.Application.Abstractions.Messaging;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Features.Customer.Queries.GetAddressByCustomerId
{
    public class GetAddressByCustomerIdQuery : IQuery<IEnumerable<CustomerAddressQueryResponse>>
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        public string? Name { get; set; }
    }
}
