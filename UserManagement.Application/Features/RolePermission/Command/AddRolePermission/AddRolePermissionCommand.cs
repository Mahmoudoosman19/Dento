using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.RolePermission.Command.AddRolePermission
{
    public class AddRolePermissionCommand
    {
        public long RoleId { get; set; }
        public long PermissionId { get; private set; }
    }
}
