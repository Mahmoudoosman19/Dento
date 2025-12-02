using Common.Domain.Repositories;
using UserManagement.Application.Abstractions;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Seeders
{
    public class RolesPermissionsSeeder : ISeeder
    {
        private readonly IGenericRepository<RolePermission> _rolePermissionRepo;
        private readonly IGenericRepository<Role> _roleRepo;
        private readonly IGenericRepository<Permission> _permissionRepo;

        public RolesPermissionsSeeder(IGenericRepository<RolePermission> rolePermissionRepo,
            IGenericRepository<Role> roleRepo, IGenericRepository<Permission> permissionRepo)
        {
            _rolePermissionRepo = rolePermissionRepo;
            _roleRepo = roleRepo;
            _permissionRepo = permissionRepo;
        }
        public int ExecutionOrder { get; set; } = 3;
        public async Task SeedAsync()
        {
            var adminPermissions = new List<RolePermissionSeederDto>
            {
                new RolePermissionSeederDto("Admin", "Supervisor.Index"),
            };

            var superVisorPermissions = new List<RolePermissionSeederDto>
            {
                new RolePermissionSeederDto("Supervisor","Designer.Index"),
                new RolePermissionSeederDto("Supervisor","Designer.Create"),
                new RolePermissionSeederDto("Supervisor","Designer.Edit"),
                new RolePermissionSeederDto("Supervisor","Designer.Details"),
                new RolePermissionSeederDto("Supervisor","Customer.Index"),
                new RolePermissionSeederDto("Supervisor","Customer.Details"),
                new RolePermissionSeederDto("Supervisor","Cases.Index"),
               
            };

            var designerPermissions = new List<RolePermissionSeederDto>
            {
                new RolePermissionSeederDto("Designer","Customer.Index"),
                new RolePermissionSeederDto("Designer","Customer.Details"),
                new RolePermissionSeederDto("Designer","Cases.Index"),

            };

            var customerPermissions = new List<RolePermissionSeederDto>() {
             

            };

            var rolesPermissionsList = new List<List<RolePermissionSeederDto>>
            {
                  adminPermissions,
                  superVisorPermissions,
                  designerPermissions,
                  customerPermissions
            };


            var storedRolesPermissions = _rolePermissionRepo.Get().ToList();
            var roles = _roleRepo.Get().ToList();
            var permissions = _permissionRepo.Get().ToList();

            var newRolesPermissions = new List<RolePermission>();

            newRolesPermissions.AddRange(
                    rolesPermissionsList
                        .SelectMany(rolesPermissions => rolesPermissions)
                        .Select(rolePermission =>
                        {
                            var role = roles.FirstOrDefault(x => x.NameEn.Trim().ToUpper().Equals(rolePermission.RoleName.Trim().ToUpper()));
                            var permission = permissions.FirstOrDefault(x => x.Name.Trim().ToUpper().Equals(rolePermission.PermissionName.Trim().ToUpper()));
                            return new { role, permission };
                        })
                        .Where(x => x.role != null && x.permission != null)
                        .Where(x => !storedRolesPermissions.Any(rp => rp.RoleId == x.role!.Id && rp.PermissionId == x.permission!.Id))
                        .Select(x => new RolePermission(x.role!.Id, x.permission!.Id))
                );


            await _rolePermissionRepo.AddRangeAsync(newRolesPermissions);
            await _rolePermissionRepo.SaveChangesAsync();
        }
    }

    public class RolePermissionSeederDto
    {
        public string RoleName { get; init; }
        public string PermissionName { get; init; }

        public RolePermissionSeederDto(string roleName, string permissionName)
        {
            RoleName = roleName;
            PermissionName = permissionName;
        }
    }
}
