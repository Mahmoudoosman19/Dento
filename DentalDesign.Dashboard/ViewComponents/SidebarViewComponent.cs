using DentalDesign.Dashboard.Models.Component;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserManagement.Domain.Entities;

namespace DentalDesign.Dashboard.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
             var userName = User.FindFirstValue("UserName") ?? User.Identity?.Name ?? "User";
             var fullNameEn = User.FindFirstValue("FullNameEn") ?? userName;
             var profileImg = "https://i.pravatar.cc/100";
             var role = User.FindFirstValue("Role") ?? "Guest";


            var model = new SidebarViewModel
             {
                 FullName = fullNameEn,
                 ProfileImageUrl = profileImg,
                 Role = role
             };

             return View(model);
        }

        private ClaimsPrincipal User => HttpContext.User;
    }
}
