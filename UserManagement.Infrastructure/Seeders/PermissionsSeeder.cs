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
                new Permission("إنشاء كوبون", "CreateCoupon"),
                new Permission("تحديث كوبون", "UpdateCoupon"),
                new Permission("حذف كوبون", "DeleteCoupon"),
                new Permission("إنشاء منتج", "CreateProduct"),
                new Permission("تحديث منتج", "UpdateProduct"),
                new Permission("حذف منتج", "DeleteProduct"),
                new Permission("إنشاء مجموعة أحجام", "CreateSizeGroup"),
                new Permission("تحديث مجموعة أحجام", "UpdateSizeGroup"),
                new Permission("حذف مجموعة أحجام", "DeleteSizeGroup"),
                new Permission("احصل على المنتجات", "GetProducts"),
                new Permission("احصل على تفاصيل المنتج", "GetProductDetails"),
                new Permission("تنشيط الكوبون", "ActivateCoupon"),
                new Permission("احصل على مجموعات الأحجام", "GetSizeGroups"),
                new Permission("انشاء فئة", "CreateCategory"),
                new Permission("تحديث فئة", "UpdateCategory"),
                new Permission("احصل علي الكوبونات", "GetListCoupon"),
                new Permission("احصل على تفاصيل مجموعة الأحجام", "GetSizeGroupDetails"),
                new Permission("تفعيل فئة", "ActivateCategory"),
                new Permission("احصل علي تفاصيل فئة", "GetCategoryDetails"),
                new Permission("احصل علي الفئات", "GetListCategory"),
                new Permission("تفعيل الاعلان", "ToggleActivation"),
                new Permission("اضافه الاعلان", "AddAdvertisement"),
                new Permission("تفعيل المنزلق", "ToggleActivationSlider"),
                new Permission("احصل عل قائمه من المنزلق", "GetListSlider"),
                new Permission("إنشاء سؤال", "AddQuestions"),
                new Permission("احصل علي قائمه من الاساله", "GetListQuestions"),
                new Permission("احصل علي قائمه من المنتجات", "GetListProducts"),
                new Permission("احصل علي قائمه من الفئات للمشاهدة", "CategoryListLookup"),
                new Permission("احصل علي قائمه من الأحجام للمشاهدة", "GetSizeGroupsLookup"),
                new Permission("احصل علي تفاصيل الكوبون", "GetCouponDetails"),
                new Permission("إعادة تخزين المنتج", "RestockProduct"),
                new Permission("احصل علي الاحجام", "GetSizes"),
                new Permission("احصل علي النقاط", "GetPointsLookup"),
                new Permission("احصل علي الالوان", "GetColorsLookup"),
                new Permission("تفعيل المنتج", "ToggleActivationProduct"),
                new Permission("إضافة تقييم للمنتج", "AddProductReview"),
                new Permission("إنشاء كلمة مرور لمرة واحدة", "CreateOTP"),
                new Permission("احصل علي قائمة من البائعين", "GetVendorList"),
                new("اٍنشاء منتج من مشرف", "SupervisorCreateProduct"),
                new(" الموافقه علي المنتج ", "SupervisorProductAdmission"),
                new("رفض منتج", "RejectProduct"),
                new("المفضل للمستخدم","ToggleUserFavorite"),
                new(" احصل علي قئمه من المفضل","GetListUserFavorite"),
                new("تسجيل الدخول العميل","LoginCustomer"),
                new("حذف الكوبون","Delete Coupon"),
                new("احصل علي قائمه طلبات للعميل","GetCustomerOrderList"),
                new("تغيير الباسورد","ChangePassword"),
                new("معلومات اضافيه للزبون", "CustomerExtraDetails"),
                new("تعليقات علي المنتجات المرفوضه","getCommentsProducts"),
                 new("تفاصيل المنتج","ProductCustomerDetails"),
                  new("لوحة التحكم إلغاء الطلب","DashboardCancelOrder"),
                  new("العميل الغاء الطلب","CustomerCancelOrder"),
                new ("حذف التعليقات","DeleteComment"),
                new("لوحة التحكم إلغاء الطلب","DashboardCancelOrder"),
                new("تفاصيل المنتج","ProductCustomerDetails"),
                 new("عرض المشرف لقائمة الطلب","AdminViewListOrder"),
                new("تفاصيل الطلب","GetOrderDetails"),
                new("لوحة التحكم عرض تفاصيل الطلب","DashboardViewOrderDetails"),
                new("عرض تقييم المنتج","GetProductReview"),
                 new("عرض قائمه الطلب","GetAdminOrderList"),
                 new("عرض قائمه طلبات البائع","VendorGetOrders"),
                 new("عرض قائمه طلبات العميل","CustomerGetOrders"),
                 new("تحديث بيانات المستخدم","UpdateUserProfile"),
                 new(" انشاء عنوان","AddAddress"),
                 new(" تعديل عنوان","EditAddress"),
                  new(" مسح عنوان","DeleteAddress"),
                 new(" قائمه العنوان","ListAddress"),
                 new(" طريقه الدفع","CheckOut"),
                 new("اضافة نطاق للنقاط", "AddPointsRange"),
                 new("عرض قائمة النقاط", "GetAllPointsRange"),
                 new("الحصول علي جميع صور الاعلانات","GetAllAdvertisementImage"),
                 new("الحصول على دور لوحة التحكم","GetDashboardRole"),
                 new("اضافه الي دولابي","AddDolaby"),
                 new("قائمه ملابس دولابي","GetDolaby"),
                 new("ازاله من دولابي","RemoveDolaby"),
                 new("بيانات المستخدم","GetUserData"),
                 new("اضافة الي البروفة","AddToBrova"),
                 new("ازاله  البروفة","RemoveBrova"),
                 new("الحصول عل كل الاعلانات","GetListAdvertisement"),
                 new("الحصول عل معلومات البائع","GetVendorInformation"),
                 new("عرض عناصر البروفة", "GetBrovaItems"),
                 new("جلب البائع للكوبونات","VendorgGetCoupon"),
                 new("تنشيط كوبون البائع","VendorActivation"),
                 new("الحصول عل كل المنتجات بتاع البائع","GetProductByVendorId"),
                 new("انشاء البائع الكوبونات","VendorCreateCoupon"),
                 new("الحصول عل قائمه من الاوردر","AdminGetOrderslist"),
                 new("الحصول عل قائمه من المستخدمين","GetUsers"),
                 new("تاكيد اسنلام الاوردر","CustomerReceivedOrder"),
                 new("قيام البائع لتأكيد طلب التسليم","VendorConfirmsDeliveryOfOrdersToShippingCompany"),
                 new("ارسال المشرف الاوردارات الي شركه الشحن","AdminSendesOrders"),
                 new("احصل على إحصائيات المنتج","GetProductStatistics"),
                 new("احصل على أفضل البائعين","GetTopVendors"),
                 new("إحصائيات الطلبات التي تم تسليمها شهريًا","MonthlyDeliveredOrderStatistics"),
                 new("الحصول علي صور الافاتار","GetAvatarImages"),
                    new("اضافه صوره افاتار","AddAvatarImage"),
                    new("انشاء حساب للبائع","RegisterVendorAccount"),
                    new("اغلاق اوردارات","AdminClosingOrder"),
                    new("احصل على  محفظة الستخدم","GetUserWallet"),
                    new("احصل على معاملة محفظة الستخدم","GetUserWalletTransaction"),
                    new("حصول البائع علي المنتجات التي ت توصيلها","vendorgetListorderdelivered"),
                    new("الحصول علي اكنر خمس منتجات مبيعا","getvendortopproduct"),
                    new("التحقق من وجود صوره للمستخدم","CheckIfCustomerHasAvatar"),
                    new("تعديل تقييم للمنتج","UpdateProductReview"),
                    new("احصل علي تفاصيل الكوبون بالكود","GetCouponDetailsByCode"),
                    new("اكسيل شييت بالاوردارات التي سيتم توصيلها غدا","Excelsheetoforderstobedeliveredtomorrow"),
                    new("الوصول إلى الشاشه الرائسيه","AccessHome"),
            };

            var existingPermissions = _permissionRepo.Get();

            var newPermissions = permissions
                .Where(p => !existingPermissions
                    .Any(ep => ep.NameEn == p.NameEn && ep.NameAr == p.NameAr))
                .ToList();

            if (newPermissions.Any())
            {
                await _permissionRepo.AddRangeAsync(newPermissions);
                await _permissionRepo.SaveChangesAsync();
            }
        }
    }
}
