using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.User.Commands.AddUser
{
    public sealed class AddUserCommand : ICommand
    {
        [DisplayName("الاسم باللغة الانجليزية")]
        public string? FullNameEn { get; init; }
        [DisplayName("الجنس")]
        public UserGender Gender { get; set; }
        [DisplayName("تاريخ الميلاد")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }


    }
}
