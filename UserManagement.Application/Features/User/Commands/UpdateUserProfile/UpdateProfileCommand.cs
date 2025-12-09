using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UserManagement.Domain.Enums;
using ICommand = Common.Application.Abstractions.Messaging.ICommand;

namespace UserManagement.Application.Features.User.Commands.UpdateUserProfile
{
    public sealed class UpdateProfileCommand : ICommand
    {
        [DisplayName("Name")]
        public string? FullNameEn { get; init; }
        [DisplayName("Gender")]
        public UserGender Gender { get; set; }
        [DisplayName("Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }
        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }

    }
}
