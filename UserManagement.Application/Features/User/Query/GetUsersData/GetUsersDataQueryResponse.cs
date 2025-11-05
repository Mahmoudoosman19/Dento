namespace UserManagement.Application.Features.User.Queries.GetUsersData
{
    public class GetUsersDataQueryResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string FullNameAr { get; set; } = null!;
        public string FullNameEn { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Status { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
