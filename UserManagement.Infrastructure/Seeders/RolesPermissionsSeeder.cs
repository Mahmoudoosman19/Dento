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
                new RolePermissionSeederDto("Admin", "CreateCoupon"),
                new RolePermissionSeederDto("Admin", "UpdateCoupon"),
                new RolePermissionSeederDto("Admin", "DeleteCoupon"),
                new RolePermissionSeederDto("Admin", "CreateProduct"),
                new RolePermissionSeederDto("Admin", "UpdateProduct"),
                new RolePermissionSeederDto("Admin", "DeleteProduct"),
                new RolePermissionSeederDto("Admin", "CreateSizeGroup"),
                new RolePermissionSeederDto("Admin", "UpdateSizeGroup"),
                new RolePermissionSeederDto("Admin", "DeleteSizeGroup"),
                new RolePermissionSeederDto("Admin", "CreateCategory"),
                new RolePermissionSeederDto("Admin", "UpdateCategory"),
                new RolePermissionSeederDto("Admin", "ActivateCategory"),
                new RolePermissionSeederDto("Admin", "GetSizeGroupDetails"),
                new RolePermissionSeederDto("Admin", "GetSizeGroups"),
                new RolePermissionSeederDto("Admin", "ToggleAdvertisementActivation"),
                new RolePermissionSeederDto("Admin", "AddAdvertisement"),
                new RolePermissionSeederDto("Admin", "ToggleActivationSlider"),
                new RolePermissionSeederDto("Admin", "GetListSlider"),
                new RolePermissionSeederDto("Admin", "AddQuestion"),
                new RolePermissionSeederDto("Admin", "GetListQuestions"),
                new RolePermissionSeederDto("Admin", "GetListCoupon"),
                new RolePermissionSeederDto("Admin", "CategoryListLookup"),
                new RolePermissionSeederDto("Admin", "GetSizeGroupsLookup"),
                new RolePermissionSeederDto("Admin", "GetCouponDetails"),
                new RolePermissionSeederDto("Admin", "GetSizes"),
                new RolePermissionSeederDto("Admin", "GetColorsLookup"),
                new RolePermissionSeederDto("Admin", "ToggleActivationProduct"),
                new RolePermissionSeederDto("Admin", "GetVendorList"),
                new RolePermissionSeederDto("Admin", "SupervisorCreateProduct"),
                new RolePermissionSeederDto("Admin", "SupervisorProductAdmission"),
                new RolePermissionSeederDto("Admin", "RejectProduct"),
                new RolePermissionSeederDto("Admin", "ToggleUserFavorite"),
                new RolePermissionSeederDto("Admin", "GetListUserFavorite"),
                new RolePermissionSeederDto("Admin", "ChangePassword"),
                new RolePermissionSeederDto("Admin","getCommentsProducts"),
                new RolePermissionSeederDto("Admin","DeleteComment"),
                new RolePermissionSeederDto("Admin","ProductCustomerDetails"),
                new RolePermissionSeederDto("Admin","DashboardCancelOrder"),
                new RolePermissionSeederDto("Admin","AdminViewListOrder"),
                new RolePermissionSeederDto("Admin","DashboardViewOrderDetails"),
               // new RolePermissionSeederDto("Admin","GetProductReview"),
                new RolePermissionSeederDto("Admin","UpdateUserProfile"),
                new RolePermissionSeederDto("Admin","AddAddress"),
                new RolePermissionSeederDto("Admin","EditAddress"),
                new RolePermissionSeederDto("Admin","DeleteAddress"),
                new RolePermissionSeederDto("Admin","ListAddress"),
                new RolePermissionSeederDto("Admin","AddPointsRange"),
                new RolePermissionSeederDto("Admin","GetAllPointsRange"),
                new RolePermissionSeederDto("Admin","GetAllAdvertismentImage"),
                new RolePermissionSeederDto("Admin","GetDashboardRole"),
                new RolePermissionSeederDto("Admin","AdminGetOrderslist"),
                new RolePermissionSeederDto("Admin","GetUsers"),
                new RolePermissionSeederDto("Admin","AdminSendesOrders"),
                new RolePermissionSeederDto("Admin","GetListAdvertisement"),
                new RolePermissionSeederDto("Admin","GetProductStatistics"),
                new RolePermissionSeederDto("Admin","GetTopVendors"),
                new RolePermissionSeederDto("Admin","MonthlyDeliveredOrderStatistics"),
                new RolePermissionSeederDto("Admin","RegisterVendorAccount"),
                new RolePermissionSeederDto("Admin","AdminClosingOrder"),
                new RolePermissionSeederDto("Admin","CheckIfCustomerHasAvatar"),
                new RolePermissionSeederDto("Admin","UpdateProductReview"),
                new RolePermissionSeederDto("Admin","GetCouponDetailsByCode"),
                new RolePermissionSeederDto("Admin","Excelsheetoforderstobedeliveredtomorrow"),
                new RolePermissionSeederDto("Admin","AccessHome"),
            };

            var superVisorPermissions = new List<RolePermissionSeederDto>
            {
                new RolePermissionSeederDto("SuperVisor", "CreateCoupon"),
                new RolePermissionSeederDto("SuperVisor", "UpdateCoupon"),
                new RolePermissionSeederDto("SuperVisor", "DeleteCoupon"),
                new RolePermissionSeederDto("SuperVisor", "CreateProduct"),
                new RolePermissionSeederDto("SuperVisor", "UpdateProduct"),
                new RolePermissionSeederDto("SuperVisor", "DeleteProduct"),
                new RolePermissionSeederDto("SuperVisor", "CreateSizeGroup"),
                new RolePermissionSeederDto("SuperVisor", "UpdateSizeGroup"),
                new RolePermissionSeederDto("SuperVisor", "DeleteSizeGroup"),
                new RolePermissionSeederDto("SuperVisor", "GetProductDetails"),
                new RolePermissionSeederDto("SuperVisor", "GetListProducts"),
                new RolePermissionSeederDto("SuperVisor", "GetSizeGroupDetails"),
                new RolePermissionSeederDto("SuperVisor", "GetSizeGroups"),
                new RolePermissionSeederDto("SuperVisor", "CategoryListLookup"),
                new RolePermissionSeederDto("SuperVisor", "GetSizeGroupsLookup"),
                new RolePermissionSeederDto("SuperVisor", "GetSizes"),
                new RolePermissionSeederDto("SuperVisor", "GetPointsLookup"),
                new RolePermissionSeederDto("SuperVisor", "GetColorsLookup"),
                new RolePermissionSeederDto("SuperVisor", "GetVendorList"),
                new RolePermissionSeederDto("SuperVisor", "SupervisorCreateProduct"),
                new RolePermissionSeederDto("SuperVisor", "SupervisorProductAdmission"),
                new RolePermissionSeederDto("SuperVisor", "RejectProduct"),
                new RolePermissionSeederDto("SuperVisor", "ChangePassword"),
                 new RolePermissionSeederDto("SuperVisor", "ToggleActivationProduct"),
                 new RolePermissionSeederDto("SuperVisor", "getCommentsProducts"),
                 new RolePermissionSeederDto("SuperVisor","DeleteComment"),
                 new RolePermissionSeederDto("SuperVisor","UpdateUserProfile"),
                 new RolePermissionSeederDto("SuperVisor","GetDashboardRole"),
                 new RolePermissionSeederDto("SuperVisor","GetProductStatistics"),
                 new RolePermissionSeederDto("SuperVisor","GetTopVendors"),
                 new RolePermissionSeederDto("SuperVisor","MonthlyDeliveredOrderStatistics"),
                 new RolePermissionSeederDto("SuperVisor","RegisterVendorAccount"),
                 new RolePermissionSeederDto("SuperVisor","CheckIfCustomerHasAvatar"),
                 new RolePermissionSeederDto("SuperVisor","RestockProduct"),
                 new RolePermissionSeederDto("SuperVisor","GetCouponDetailsByCode"),
                 new RolePermissionSeederDto("SuperVisor","AccessHome"),


            };

            var vendorPermissions = new List<RolePermissionSeederDto>
            {
                new RolePermissionSeederDto("Designer", "CreateProduct"),
                new RolePermissionSeederDto("Designer", "UpdateProduct"),
                new RolePermissionSeederDto("Designer", "DeleteProduct"),
                new RolePermissionSeederDto("Designer", "CreateCoupon"),
                new RolePermissionSeederDto("Designer", "GetProductDetails"),
                new RolePermissionSeederDto("Designer", "GetListProducts"),
                new RolePermissionSeederDto("Designer", "GetListCoupon"),
                new RolePermissionSeederDto("Designer", "CategoryListLookup"),
                new RolePermissionSeederDto("Designer", "GetSizes"),
                new RolePermissionSeederDto("Designer", "GetPointsLookup"),
                new RolePermissionSeederDto("Designer", "GetColorsLookup"),
                new RolePermissionSeederDto("Designer", "RestockProduct"),
                new RolePermissionSeederDto("Designer", "GetCouponDetails"),
                new RolePermissionSeederDto("Designer", "GetSizeGroupsLookup"),
                new RolePermissionSeederDto("Designer", "ToggleActivationProduct"),
                new RolePermissionSeederDto("Designer", "ChangePassword"),
                new RolePermissionSeederDto("Designer", "getCommentsProducts"),
                 new RolePermissionSeederDto("Designer", "DashboardCancelOrder"),
                  new RolePermissionSeederDto("Designer", "DashboardViewOrderDetails"),
              //   new RolePermissionSeederDto("Designer", "GetProductReview"),
                  new RolePermissionSeederDto("Designer", "VendorGetOrders"),
                  new RolePermissionSeederDto("Designer", "UpdateUserProfile"),
                  new RolePermissionSeederDto("Designer", "GetDashboardRole"),
                  new RolePermissionSeederDto("Designer", "VendorgGetCoupon"),
                  new RolePermissionSeederDto("Designer", "VendorActivation"),
                  new RolePermissionSeederDto("Designer", "VendorCreateCoupon"),
                  new RolePermissionSeederDto("Designer", "GetUserWallet"),
                  new RolePermissionSeederDto("Designer", "GetUserWalletTransaction"),
                  new RolePermissionSeederDto("Designer", "CheckIfCustomerHasAvatar"),
                  new RolePermissionSeederDto("Designer", "vendorgetListorderdelivered"),
                  new RolePermissionSeederDto("Designer", "getvendortopproduct"),
                  new RolePermissionSeederDto("Designer", "AdminGetOrderslist"),
                  new RolePermissionSeederDto("Designer", "UpdateProductReview"),
                  new RolePermissionSeederDto("Designer", "GetCouponDetailsByCode"),
                  new RolePermissionSeederDto("Designer", "AccessHome"),




            };

            var customerPermissions = new List<RolePermissionSeederDto>() {
                new RolePermissionSeederDto("Customer", "ToggleUserFavorite"),
                new RolePermissionSeederDto("Customer", "GetListUserFavorite"),
                new RolePermissionSeederDto("Customer", "LoginCustomer"),
                new RolePermissionSeederDto("Customer","GetCustomerOrderList"),
                new RolePermissionSeederDto("Customer", "ChangePassword"),
                new("Customer", "CustomerExtraDetails"),
                new RolePermissionSeederDto("Customer", "ProductCustomerDetails"),
                new RolePermissionSeederDto("Customer", "GetOrderDetails"),
                new RolePermissionSeederDto("Customer", "CustomerCancelOrder"),
                new RolePermissionSeederDto("Customer", "UpdateUserProfile"),
                new RolePermissionSeederDto("Customer", "AddAddress"),
                new RolePermissionSeederDto("Customer", "CustomerGetOrders"),
                new RolePermissionSeederDto("Customer", "EditAddress"),
                new RolePermissionSeederDto("Customer", "ListAddress"),
                new RolePermissionSeederDto("Customer", "CheckOut"),
                new RolePermissionSeederDto("Customer", "DeleteAddress"),
                new RolePermissionSeederDto("Customer","GetDashboardRole"),
                new RolePermissionSeederDto("Customer","AddDolaby"),
                new RolePermissionSeederDto("Customer","GetDolaby"),
                new RolePermissionSeederDto("Customer","RemoveDolaby"),
                new RolePermissionSeederDto("Customer","GetUserData"),
                new RolePermissionSeederDto("Customer","AddToBrova"),
                new RolePermissionSeederDto("Customer","RemoveBrova"),
                new RolePermissionSeederDto("Customer","GetBrovaItems"),
                new RolePermissionSeederDto("Customer","GetProductByVendorId"),
                new RolePermissionSeederDto("Customer","GetVendorInformation"),
                new RolePermissionSeederDto("Customer","CustomerReceivedOrder"),
                new RolePermissionSeederDto("Customer","GetProductStatistics"),
                new RolePermissionSeederDto("Customer","GetTopVendors"),
                new RolePermissionSeederDto("Customer","MonthlyDeliveredOrderStatistics"),
                new RolePermissionSeederDto("Customer","GetAvatarImages"),
                new RolePermissionSeederDto("Customer","AddAvatarImage"),
                new RolePermissionSeederDto("Customer","CheckIfCustomerHasAvatar"),
                new RolePermissionSeederDto("Customer","UpdateProductReview"),
                new RolePermissionSeederDto("Customer","GetCouponDetailsByCode"),
                  new RolePermissionSeederDto("Customer", "AddCase"),

            };

            var rolesPermissionsList = new List<List<RolePermissionSeederDto>>
            {
                  adminPermissions,
                  superVisorPermissions,
                  vendorPermissions,
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
