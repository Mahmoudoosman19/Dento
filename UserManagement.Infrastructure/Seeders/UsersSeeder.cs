using Common.Domain.Repositories;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Identity;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Infrastructure.Seeders
{
    public class UsersSeeder : ISeeder
    {
        private readonly CustomUserManager _userManager;
        private readonly IGenericRepository<Role> _roleRepo;

        public UsersSeeder(CustomUserManager userManager, IGenericRepository<Role> roleRepo)
        {
            _userManager = userManager;
            _roleRepo = roleRepo;
        }
        public int ExecutionOrder { get; set; } = 4;
        public async Task SeedAsync()
        {
            var users = new List<User>
            {
                new User("admin", "admin@admin.com", "01144415599","Admin User", "مستخدم المسؤول", UserStatus.Active, UserGender.Male),
                new User("supervisor", "supervisor@supervisor.com", "01144425599", "Supervisor User", "مستخدم المشرف", UserStatus.Active, UserGender.Male),
                new User("vendor", "vendor@vendor.com", "01144435599", "Vendor User", "مستخدم البائع", UserStatus.Active, UserGender.Male),
                new User("customer", "customer@customer.com", "01144445599", "Customer User", "مستخدم العميل", UserStatus.Active, UserGender.Male)
            };

            var roles = _roleRepo.Get();

            foreach (var user in users)
            {
                if (await _userManager.IsUserExistByEmailAsync(user.Email!))
                    continue;

                var role = roles.FirstOrDefault(x => x.NameEn.ToUpper().Equals(user.UserName!.ToUpper()));
                user.AssignRole(role!.Id);
                user.ConfirmEmail();
                user.ConfirmPhoneNumber();

                await _userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
