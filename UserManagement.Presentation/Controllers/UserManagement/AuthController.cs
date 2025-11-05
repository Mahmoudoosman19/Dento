using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.User.Commands.AddUser;

namespace UserManagement.Presentation.Controllers.UserManagement
{
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
        public AuthController(ISender sender) : base(sender)
        {
        }

        [AllowAnonymous]
        [HttpPost("AddUser")]
        public async Task<IActionResult> UpdateProfile([FromBody] AddUserCommand updatedProfile, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(updatedProfile);
            return HandleResult(response);
        }
    }
}
