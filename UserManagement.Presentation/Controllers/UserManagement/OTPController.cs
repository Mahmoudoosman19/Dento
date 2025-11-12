using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.OTP.Commands.ConfirmOTP;
using UserManagement.Application.Features.OTP.Commands.CreateOTP;

namespace UserManagement.Presentation.Controllers
{
    [Route("api/UserManagement/[controller]")]
    public sealed class OTPController : ApiController
    {
        public OTPController(ISender sender) : base(sender)
        {
        }

        [HttpPost("create-otp")]
        public async Task<IActionResult> CreateOTP(CreateOTPCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }

        [HttpPut("confirm-otp")]
        public async Task<IActionResult> ConfirmOTP(ConfirmOTPCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }

    }
}
