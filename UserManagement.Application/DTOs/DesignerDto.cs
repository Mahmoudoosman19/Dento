using UserManagement.Domain.Enums;

namespace UserManagement.Application.DTOs
{
    public class DesignerDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string FullNameEn { get; set; } = null!;
        public string FullNameAr { get; set; } = null!;
        public UserStatus Status { get; set; }
        public UserGender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string FrontIdImageName { get; set; } = null!;
        public string BackIdImageName { get; set; } = null!;
        public string LogoImageName { get; set; } = null!;
        public string CompanyNameAr { get; set; } = null!;
        public string CompanyNameEn { get; set; } = null!;
        public string Address { get; set; } = null!;
        public double Rate { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
