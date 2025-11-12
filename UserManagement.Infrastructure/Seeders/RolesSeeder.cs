using Common.Domain.Repositories;
using UserManagement.Application.Abstractions;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Seeders
{
    public class RolesSeeder : ISeeder
    {
        private readonly IGenericRepository<Role> _roleRepo;

        public RolesSeeder(IGenericRepository<Role> roleRepo)
        {
            _roleRepo = roleRepo;
        }
        public int ExecutionOrder { get; set; } = 1;
        public async Task SeedAsync()
        {
            var roles = new List<Role>()
            {
                new Role("مدير", "Admin"),
                new Role("مشرف", "SuperVisor"),
                new Role("بائع", "Vendor"),
                new Role("زبون", "Customer"),
            };

            var existingRoles = _roleRepo.Get();

            var newRoles = roles
                .Where(r => !existingRoles
                    .Any(er => er.NameEn == r.NameEn && er.NameAr == r.NameAr))
                .ToList();

            if (newRoles.Any())
            {
                await _roleRepo.AddRangeAsync(newRoles);
                await _roleRepo.SaveChangesAsync();
            }

        }
    }
}
