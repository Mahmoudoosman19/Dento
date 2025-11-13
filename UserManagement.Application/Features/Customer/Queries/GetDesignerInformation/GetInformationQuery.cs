using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.Customer.Queries.GetVendorInformation
{
    public class GetInformationQuery:IQuery<GetInformationResponse>
    {
        public Guid VendorId {  get; set; }   
    }
}
