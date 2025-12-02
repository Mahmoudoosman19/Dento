using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalDesign.Dashboard.Controllers
{
    [Authorize(Policy = "DynamicActionPermission")] 
    public class BaseController : Controller
    {
        protected readonly ISender Sender;

        public BaseController(ISender sender)
        {
            Sender = sender;
        }
    }
}
