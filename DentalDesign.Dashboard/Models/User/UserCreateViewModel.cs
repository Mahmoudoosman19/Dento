using System.ComponentModel.DataAnnotations;
using UserManagement.Domain.Enums;

namespace DentalDesign.Dashboard.Models.User
{
    public class UserCreateViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Full Name (English)")]
        public string FullNameEn { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Full Name (Arabic)")]
        public string FullNameAr { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public UserGender Gender { get; set; }
    }
}
