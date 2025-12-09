using Case.Application.Features.Case.Query.GetCases;
using Case.Application.Features.Case.Query.GetCasesByStatusId;
using Case.Domain.Enum;
using DentalDesign.Dashboard.Models;
using DentalDesign.Dashboard.Models.Case;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserManagement.Application.Features.User.Commands.UpdateUserProfile;
using UserManagement.Application.Features.User.Queries.GetUserData;

namespace DentalDesign.Dashboard.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(ISender sender) : base(sender)
        {
        }

        public IActionResult Index()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_IndexPartial");
            return View();
        }

        public async Task<IActionResult> GetAllCases()
        {
            var query = new GetCasesQuery();
            var result = await Sender.Send(query);
            return Json(result);
        }
        public async Task<IActionResult> GetCasesByStatus([FromQuery] long statusId)
        {
            var query = new GetCasesByStatusIdQuery { StatusId = statusId };

            var result = await Sender.Send(query);
            return Json(result);
        }

        public async Task<IActionResult> StatusList([FromQuery] long statusId)
        {
            var query = new GetCasesByStatusIdQuery { StatusId = statusId };
            var result = await Sender.Send(query);

            var vm = new List<CaseViewModel>();
            foreach (var c in result.Data)
            {
                string designerName = "Not Assigned";
                if (c.DesignertId != null)
                {
                    var designer = await Sender.Send(new GetUserDataQuery { Id = c.DesignertId.Value });
                    designerName = designer?.Data?.FullNameEn ?? "Unknown";
                }

                vm.Add(new CaseViewModel //case
                {
                    Id = c.Id,
                    CaseName = c.CaseName,
                    DueDate = c.DueDate,
                    CreatedOnUtc = c.CreatedOnUtc,
                    StatusId = c.StatusId,
                    DesignerId = c.DesignertId,
                    DesignerName = designerName,
                    CaseType = c.CaseType,
                    AssignedAt = c.AssignedAt
                });
            }

            ViewBag.Status = ((CaseStatusEnum)statusId).ToString();
            return PartialView("_CasesListPartial", vm);
        }


    



    }
}
