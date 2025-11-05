namespace IdentityHelper.Models
{
    public class UserModel
    {
        public Guid Id { get; set; } 
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string FullNameAr { get; set; } = null!;
        public string FullNameEn { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string CreatedOnUtc { get; set; } = null!;
        public string? ModifiedOnUtc { get; set; }
    }
}
