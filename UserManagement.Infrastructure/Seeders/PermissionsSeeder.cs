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
                new Permission( "CreateCoupon"),
                new Permission( "UpdateCoupon"),
                new Permission("DeleteCoupon"),
                new Permission("CreateProduct"),
                new Permission("UpdateProduct"),
                new Permission("DeleteProduct"),
                new Permission("CreateSizeGroup"),
                new Permission("UpdateSizeGroup"),
                new Permission("DeleteSizeGroup"),
                new Permission("GetProducts"),
                new Permission("GetProductDetails"),
                new Permission("ActivateCoupon"),
                new Permission( "GetSizeGroups"),
                new Permission("CreateCategory"),
                new Permission("UpdateCategory"),
                new Permission("GetListCoupon"),
                new Permission("GetSizeGroupDetails"),
                new Permission("ActivateCategory"),
                new Permission("GetCategoryDetails"),
                new Permission("GetListCategory"),
                new Permission("ToggleActivation"),
                new Permission("AddAdvertisement"),
                new Permission("ToggleActivationSlider"),
                new Permission("GetListSlider"),
                new Permission( "AddQuestions"),
                new Permission("GetListQuestions"),
                new Permission("GetListProducts"),
                new Permission("CategoryListLookup"),
                new Permission("GetSizeGroupsLookup"),
                new Permission("GetCouponDetails"),
                new Permission("RestockProduct"),
                new Permission("GetSizes"),
                new Permission("GetPointsLookup"),
                new Permission("GetColorsLookup"),
                new Permission("ToggleActivationProduct"),
                new Permission("AddProductReview"),
                new Permission("CreateOTP"),
                new Permission("GetVendorList"),
                new("SupervisorCreateProduct"),
                new("SupervisorProductAdmission"),
                new("RejectProduct"),
                new("ToggleUserFavorite"),
                new("GetListUserFavorite"),
                new("LoginCustomer"),
                new("Delete Coupon"),
                new("GetCustomerOrderList"),
                new("ChangePassword"),
                new("CustomerExtraDetails"),
                new("getCommentsProducts"),
                 new("ProductCustomerDetails"),
                  new("DashboardCancelOrder"),
                  new("CustomerCancelOrder"),
                new ("DeleteComment"),
                new("DashboardCancelOrder"),
                new("ProductCustomerDetails"),
                 new("AdminViewListOrder"),
                new("GetOrderDetails"),
                new("DashboardViewOrderDetails"),
                new("GetProductReview"),
                 new("GetAdminOrderList"),
                 new("VendorGetOrders"),
                 new("CustomerGetOrders"),
                 new("UpdateUserProfile"),
                 new("AddAddress"),
                 new("EditAddress"),
                  new("DeleteAddress"),
                 new("ListAddress"),
                 new("CheckOut"),
                 new("AddPointsRange"),
                 new("GetAllPointsRange"),
                 new("GetAllAdvertisementImage"),
                 new("GetDashboardRole"),
                 new("AddDolaby"),
                 new("GetDolaby"),
                 new("RemoveDolaby"),
                 new("GetUserData"),
                 new("AddToBrova"),
                 new("RemoveBrova"),
                 new("GetListAdvertisement"),
                 new("GetVendorInformation"),
                 new("GetBrovaItems"),
                 new("VendorgGetCoupon"),
                 new("VendorActivation"),
                 new("GetProductByVendorId"),
                 new("VendorCreateCoupon"),
                 new("AdminGetOrderslist"),
                 new("GetUsers"),
                 new("CustomerReceivedOrder"),
                 new("VendorConfirmsDeliveryOfOrdersToShippingCompany"),
                 new("AdminSendesOrders"),
                 new("GetProductStatistics"),
                 new("GetTopVendors"),
                 new("MonthlyDeliveredOrderStatistics"),
                 new("GetAvatarImages"),
                    new("AddAvatarImage"),
                    new("RegisterVendorAccount"),
                    new("AdminClosingOrder"),
                    new("GetUserWallet"),
                    new("GetUserWalletTransaction"),
                    new("vendorgetListorderdelivered"),
                    new("getvendortopproduct"),
                    new("CheckIfCustomerHasAvatar"),
                    new("UpdateProductReview"),
                    new("GetCouponDetailsByCode"),
                    new("Excelsheetoforderstobedeliveredtomorrow"),
                    new("AccessHome"),
                    new("AddCase")
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
