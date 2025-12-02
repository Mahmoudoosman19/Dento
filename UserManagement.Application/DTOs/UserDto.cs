using UserManagement.Domain.Enums;

namespace UserManagement.Application.DTOs
{
    //customer, admin
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string FullNameEn { get; set; } = null!;
        public string FullNameAr { get; set; } = null!;
        public int Points { get; set; } = 0;
        public UserStatus Status { get; set; }
        public UserGender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public long RoleId { get; set; }
    }
}
