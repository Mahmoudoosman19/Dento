using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.User.Commands.UpdateUser
{
    public class UpdateUserCommand : ICommand<ResponseModel>
    {
        public Guid Id { get; init; }  
        public string UserName { get; init; } = string.Empty;
        public string FullNameEn { get; init; } = string.Empty;
        public string FullNameAr { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public DateTime? BirthDate { get; init; }
        public UserGender Gender { get; init; }
    }
}
