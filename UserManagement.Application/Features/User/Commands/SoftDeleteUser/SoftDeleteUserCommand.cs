using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.User.Commands.SoftDeleteUser
{
    public class SoftDeleteUserCommand : ICommand
    {
        public Guid userId { get; set; }
    }
}
