using Common.Domain.Repositories;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Identity;
using UserManagement.Application.Specifications.Designer;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Seeders
{
    internal class DesignerSeeder : ISeeder
    {
        private readonly CustomUserManager _userManager;
        private readonly IGenericRepository<Role> _roleRepo;
        private readonly IGenericRepository<Designer> _vendorRepo;

        public int ExecutionOrder { get; set; } = 5;

        public DesignerSeeder(CustomUserManager userManager, IGenericRepository<Role> roleRepo,
            IGenericRepository<Designer> vendorRepo)
        {
            _userManager = userManager;
            _roleRepo = roleRepo;
            _vendorRepo = vendorRepo;
        }

        public async Task SeedAsync()
        {
            var isDummyVendorExist = _vendorRepo.GetEntityWithSpec(new GetDesignerByUserEmailSpecification("designer@designer.com"));
            if (isDummyVendorExist != null)
                return;

            var user = await _userManager.FindByEmailAsync("designer@designer.com");
            if (user == null)
                return;

            var vendor = new Designer();
            //vendor.SetBaseAvatarId("تجربة", "test");
            //vendor.SetCustomizedAvatarId("dummy", "dummy");
            //vendor.SetFrontIdImage("dummy", "dummy");
            //vendor.SetBackIdImage("dummy", "dummy");
            //vendor.SetAddress("dummy address");
            //vendor.SetRate(4);
            //vendor.SetUserId(user.Id);

            await _vendorRepo.AddAsync(vendor);

            await _vendorRepo.SaveChangesAsync();
        }
    }
}
