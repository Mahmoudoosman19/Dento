namespace UserManagement.Application.DTOs
{
    public class CustomerAddressQueryResponse
    {
        public Guid Id { get; set; }    
        public Guid UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Floor { get; set; }
        public string City { get; set; }
        public string Name   { get; set; }
    }
}
