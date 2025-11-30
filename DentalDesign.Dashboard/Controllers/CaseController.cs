using Case.Application.Features.Case.Query.GetCases;
using IdentityHelper.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Features.User.Queries.GetUserData;

namespace DentalDesign.Dashboard.Controllers
{
    public class CaseController : Controller
    {
        private readonly ISender Sender;
        private readonly ITokenExtractor _tokenExtractor;

        public CaseController(ISender sender, ITokenExtractor tokenExtractor)
        {
            Sender = sender;
            _tokenExtractor = tokenExtractor;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCases(int pageIndex = 1, int pageSize = 20)
        {
            var userId = _tokenExtractor.GetUserId();
            var user = await Sender.Send(new GetUserDataQuery { Id = userId });

           
            // 1- Check the roles

            var query = new GetCasesQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            // 2- Designer: return only his assigned cases
            if (user.Data.Role == "Designer")
            {
                query.DesignerId = user.Data.Id;  // assuming user.Id is Guid
            }

            // 3- Admin or Supervisor will get all cases (no filter)

            var response = await Sender.Send(query);

            return Json(response);
        }
    }
}
