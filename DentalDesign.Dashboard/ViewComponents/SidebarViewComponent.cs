using Microsoft.AspNetCore.Mvc;

namespace DentalDesign.Dashboard.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {


            return View();
        }
    }
}
