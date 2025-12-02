using Common.Domain.Repositories;
using UserManagement.Application.Abstractions;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Seeders
{
    public class PermissionsSeeder : ISeeder
    {
        private readonly IGenericRepository<Permission> _permissionRepo;

        public PermissionsSeeder(IGenericRepository<Permission> permissionRepo)
        {
            _permissionRepo = permissionRepo;
        }

        public int ExecutionOrder { get; set; } = 2;

        public async Task SeedAsync()
        {
            var permissions = new List<Permission>
            {
              new Permission("Supervisor.Index"),
              new Permission("Supervisor.Create"),
              new Permission("Supervisor.Details"),
              new Permission("Supervisor.Edit"),
              new Permission("Designer.Index"),
              new Permission("Designer.Create"),
              new Permission("Designer.Details"),
              new Permission("Designer.Edit"),
              new Permission("Customer.Index"),
              new Permission("Customer.Details"),
              new Permission("Cases.Index"),
            };

            var existingPermissions = _permissionRepo.Get();

            var newPermissions = permissions
                .Where(p => !existingPermissions
                    .Any(ep => ep.Name == p.Name))
                .ToList();

            if (newPermissions.Any())
            {
                await _permissionRepo.AddRangeAsync(newPermissions);
                await _permissionRepo.SaveChangesAsync();
            }
        }
    }
}
