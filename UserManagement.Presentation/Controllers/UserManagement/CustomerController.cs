using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using UserManagement.Application.Features.Customer.Queries.GetVendorInformation;

namespace UserManagement.Presentation.Controllers
{
    [Route("api/UserManagement/[controller]")]
    public class CustomerController : ApiController
    {
        public CustomerController(ISender sender) : base(sender)
        {

        }
        [HttpGet("Get-Vendor-Information")]
        public async Task<IActionResult> GetVendorInformation([FromQuery] GetInformationQuery query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }

        //[HttpGet("Check-If-Customer-Has-Avatar")]
        //public async Task<IActionResult> AddCustomizedAvatarForCustomer([FromQuery] CheckIfCustomerHasAvatarQuery query)
        //{
        //    var result = await Sender.Send(query);
        //    return HandleResult(result);
        //}


    }

}
