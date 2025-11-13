using MediatR;
using Common.Presentation.Abstractions;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.Customer.Queries;
using UserManagement.Application.Features.Customer.Commands.CustomerDeletedAddressById;
using UserManagement.Application.Features.Customer.Commands;
using UserManagement.Application.Features.Customer.Commands.CustomerAddAddress;
using UserManagement.Application.Features.Customer.Commands.CustomerEditAddressById;
using UserManagement.Application.Features.Customer.Queries.GetAddressByCustomerId;

namespace UserManagement.Presentation.Controllers
{
    [Route("api/UserManagement/[controller]")]

    public class AddressController : ApiController
    {
        public AddressController(ISender sender) : base(sender)
        {
        }
        [HttpPost("Add-Address")]
        public async Task<IActionResult> CreateAddress([FromForm] AddAddressCommand AddAddress)
        {
            var response = await Sender.Send(AddAddress);
            return HandleResult(response);
        }
        [HttpPost("edit-Address")]
        public async Task<IActionResult> EditAddress([FromForm] EditAddressByIdCommand editAddress)
        {
            var response = await Sender.Send(editAddress);
            return HandleResult(response);
        }
        [HttpDelete("Delete-Address")]
        public async Task<IActionResult> DeleteAddress([FromForm] DeleteAddressByIdCommand deleteAddress)
        {
            var response = await Sender.Send(deleteAddress);
            return HandleResult(response);  
        }
        [HttpGet("List-Address")]
        public async Task<IActionResult> ListAddress([FromForm] GetAddressByCustomerIdQuery listAddress)
        {
            var response = await Sender.Send(listAddress);
            return HandleResult(response);
        }
    }
}
