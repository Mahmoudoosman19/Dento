using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.Register.DTOs
{
    public class BaseRegisterDto
    {
        private string _userName = null!;
        public string UserName
        {
            get => _userName;
            init => _userName = value!.Replace(" ", "");
        }
        public string FullNameEn { get; init; } = null!;
        public string FullNameAr { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string PhoneNumber { get; init; } = null!;
        public string Password { get; init; } = null!;
        public DateTime? BirthDate { get; set; }
        public UserGender Gender { get; init; }
    }
}
