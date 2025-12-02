using DentalDesign.Dashboard.Models.Component;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DentalDesign.Dashboard.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
             var userName = User.FindFirstValue("UserName") ?? User.Identity?.Name ?? "User";
             var fullNameEn = User.FindFirstValue("FullNameEn") ?? userName;
             var profileImg = "https://i.pravatar.cc/100"; // أو خذها من claim إذا متوفرة

             var model = new SidebarViewModel
             {
                 FullName = fullNameEn,
                 ProfileImageUrl = profileImg
             };

             return View(model);
        }

        private ClaimsPrincipal User => HttpContext.User;
    }
}
