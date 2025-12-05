using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.User.Queries.GetUserData
{
    public class GetUserDataQueryResponse
    {
        public Guid Id { get; set; }    
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string FullNameAr { get; set; } = null!;
        public string FullNameEn { get; set; } = null!;
        public long RoleId { get; set; } = 0;
        public string Status { get; set; }
        public int Points { get; set; }    
        public UserGender Gender { get; set; }
        public DateTime? BirthDate { get;  set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
