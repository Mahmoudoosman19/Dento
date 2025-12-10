using Azure;
using Case.Application.Features.Case.Command.AssignCaseToDesigner;
using Case.Application.Features.Case.Command.UpdateCaseStatus;
using Case.Application.Features.Case.Query.GetCaseById;
using Case.Application.Features.Case.Query.GetCases;
using Case.Application.Features.Case.Query.GetCasesAssignedToDesigner;
using Case.Domain.Enum;
using Common.Domain.Shared;
using DentalDesign.Dashboard.Models.Case;
using FileService.Abstraction;
using IdentityHelper.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Features.Designer.Queries.GetDesignerById;
using UserManagement.Application.Features.Designer.Queries.GetListDesigners;
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

            ViewBag.UserRoleId = user.Data.RoleId;
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_IndexPartial", vm);

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid caseId, long statusId)
        {
            var result = await Sender.Send(new UpdateCaseStatusCommand
            {
                CaseId = caseId,
                StatusId = statusId
            });

            if (!result.IsSuccess)
                return BadRequest(new { isSuccess = false, message = result.Message });

            // ✅ أعد.isSuccess = true
            return Ok(new
            {
                isSuccess = true,
                statusName = ((CaseStatusEnum)statusId).ToString()
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignToDesigner(Guid caseId, Guid designerId)
        {
            // تحقق من الـ role
            var userRole = User.FindFirst("Role")?.Value; // تأكد من اسم الـ claim
            if (userRole != "Admin" && userRole != "Supervisor")
                return BadRequest(new { isSuccess = false, message = "Unauthorized" });

            // إرسال الـ command
            var result = await Sender.Send(new AssignCaseToDesignerCommand
            {
                CaseId = caseId,
                DesignerId = designerId
            });

            if (!result.IsSuccess)
                return BadRequest(new { isSuccess = false, message = result.Message });

            return Ok(new { isSuccess = true });
        }


        [HttpGet]
        public async Task<IActionResult> GetDesignersList()
        {
            var response = await Sender.Send(new GetListDesignersQuery());
            return Json(response.Data.Select(d => new { id = d.Id, fullNameEn = d.FullNameEn }));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var query = new GetCaseByIdQuery { Id = id };
            var response = await Sender.Send(query);

            if (!response.IsSuccess)
            {
                return Request.Headers["X-Requested-With"] == "XMLHttpRequest"
                    ? PartialView("_CaseDetailsPartial", null)
                    : NotFound();
            }

            var designerQuery = new GetDesignerQuery { Id = (Guid)response.Data.DesignertId };
            var designerResponse = await Sender.Send(designerQuery);

            if (!string.IsNullOrEmpty(response.Data.Model3DPath))
            {
                var storage = HttpContext.RequestServices.GetRequiredService<IFileStorageService>();
                ViewBag.Model3DUrl = storage.GetSignedUrl(bucket: "Dento", response.Data.Model3DPath);
            }

            ViewBag.DesignerName = designerResponse.Data.FullNameEn;
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_CaseDetailsPartial", response.Data);

            return View(response.Data); 
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
                    CaseType = c.CaseType,
                    AssignedAt = c.AssignedAt
                });
            }
            return vm;
        }

    }
}
