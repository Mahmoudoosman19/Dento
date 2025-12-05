using Azure;
using Case.Application.Features.Case.Query.GetCases;
using Case.Application.Features.Case.Query.GetCasesAssignedToDesigner;
using Common.Domain.Shared;
using DentalDesign.Dashboard.Models.Case;
using IdentityHelper.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Features.User.Queries.GetUserData;
using static Google.Apis.Requests.BatchRequest;

namespace DentalDesign.Dashboard.Controllers
{
    public class CaseController : BaseController
    {
        private readonly ITokenExtractor _tokenExtractor;

        public CaseController(ISender sender, ITokenExtractor tokenExtractor) : base(sender)
        {
            _tokenExtractor = tokenExtractor;
        }
        //public IActionResult Index()
        //{
        //    // لو الطلب AJAX → رجّع Partial
        //    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        //        return PartialView("_IndexPartial");

        //    // لو الطلب عادي → رجّع صفحة كاملة بالـ Layout
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {

            var userId = _tokenExtractor.GetUserId();
            var user = await Sender.Send(new GetUserDataQuery { Id = userId });


            var response = await Sender.Send(new GetCasesQuery());
            if (!response.IsSuccess)
                return NotFound();

            var vm = new List<CaseViewModel>();

            if (user.Data.RoleId == 1 || user.Data.RoleId == 2)
            {
                 vm = await MapCases(response);
            }
            if (user.Data.RoleId == 3)
            {
                 var designerCases = response.Data.Where(c => c.DesignertId == userId).ToList();
                 vm = await MapCases(designerCases);
            }


            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_IndexPartial", vm);

            return View(vm);
        }

        private async Task<List<CaseViewModel>> MapCases(ResponseModel<IEnumerable<GetCasesQueryResponse>> response)
        {
            var vm = new List<CaseViewModel>();
            foreach (var c in response.Data)
            {
                string designerName = "Not Assigned";
                if (c.DesignertId != null) // Get designer name
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
                    CaseType = c.CaseType
                });
            }
            return vm;
        }

    }
}
