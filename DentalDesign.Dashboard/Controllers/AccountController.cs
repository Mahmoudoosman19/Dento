using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.Auth.Commands.Login;

namespace DentalDesign.Dashboard.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(ISender sender) : base(sender)
        {
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Sender.Send(command);

            if (!result.IsSuccess)
                return Unauthorized(result);

        
            Response.Cookies.Append("AuthToken", result.Data.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,               
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(3)
            });
            return Ok(new
            {
                token = result.Data.Token,
                user = result.Data.User
            });
        }



        [AllowAnonymous]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");

            return RedirectToAction("Login", "Account");
        }
    }
}
