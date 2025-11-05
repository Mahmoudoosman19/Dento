using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Domain.Entities
{
    public class RolePermission : Entity<long>, IAuditableEntity
    {
        public long RoleId { get; private set; }
        [ForeignKey(nameof(RoleId))]
        public virtual Role? Role { get; private set; }

        public long PermissionId { get; private set; }
        [ForeignKey(nameof(PermissionId))]
        public virtual Permission? Permission { get; private set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public RolePermission(long roleId, long permissionId)
        {
            SetRole(roleId);
            SetPermission(permissionId);
        }

        public void SetRole(long roleId)
        {
            RoleId = roleId;
        }

        public void SetPermission(long permissionId)
        {
            PermissionId = permissionId;
        }
    }
}
