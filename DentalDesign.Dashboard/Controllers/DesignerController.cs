using Case.Application.Features.Case.Query.GetCases;
using DentalDesign.Dashboard.Models.Case;
using DentalDesign.Dashboard.Models.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTOs;
using UserManagement.Application.Features.Auth.Commands.Register;
using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
using UserManagement.Application.Features.Auth.Commands.Register.DTOs;
using UserManagement.Application.Features.Designer.Queries.GetListDesigners;
using UserManagement.Application.Features.Supervisor.Queries.GetSupervisorByNameAndStatusAndRoleId;
using UserManagement.Application.Features.User.Commands.DeleteUser;
using UserManagement.Application.Features.User.Commands.UpdateUser;
using UserManagement.Application.Features.User.Queries.GetUserData;

namespace DentalDesign.Dashboard.Controllers
{
    public class DesignerController : BaseController
    {

        public DesignerController(ISender sender): base(sender) { }


        public async Task<IActionResult> Index(GetListDesignersQuery query)
        {

            var response = await Sender.Send(query);

            if (!response.IsSuccess)
            {
                return View(new List<UserDto>());
            }

            ViewBag.CurrentPage = query.PageIndex;
            ViewBag.PageSize = query.PageSize;
            ViewBag.TotalCount = response.TotalCount;
            ViewBag.TotalPages = (int)Math.Ceiling((double)response.TotalCount! / (double)query.PageSize);
            ViewBag.SearchName = query.Name;
            ViewBag.SearchStatus = query.Status;

            // لو الطلب AJAX → رجّع Partial
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_IndexPartial", response?.Data);

            // لو الطلب عادي → رجّع صفحة كاملة بالـ Layout
            return View(response.Data);
        }

        public IActionResult Create()
        {
            var model = new UserCreateViewModel();
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_CreatePartial", model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return PartialView("_CreatePartial", model);

                return View(model);
            }

            // تحويل ViewModel لـ DTO
            var designerDto = new DesignerRegisterDto
            {
                UserName = model.UserName,
                FullNameEn = model.FullNameEn,
                FullNameAr = model.FullNameAr,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                BirthDate = model.BirthDate,
                Gender = model.Gender
            };

            var command = new RegisterCommand(designerDto, RegisterType.Designer);

            var result = await Sender.Send(command);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "حدث خطأ أثناء التسجيل");

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return PartialView("_CreatePartial", model);

                return View(model);
            }


            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_IndexPartial");

            return View("Index");
        }


        // GET: /Designer/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var query = new GetUserDataQuery { Id = id };
            var response = await Sender.Send(query);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            var model = new UserEditViewModel
            {
                Id = id,
                UserName = response.Data!.UserName,
                FullNameEn = response.Data.FullNameEn,
                FullNameAr = response.Data.FullNameAr,
                Email = response.Data.Email,
                PhoneNumber = response.Data.PhoneNumber,
                BirthDate = response.Data.BirthDate,
                Gender = response.Data.Gender
            };

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_EditPartial", model);

            return View(model);
        }

        // POST: /Designer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return PartialView("_EditPartial", model);
                return View(model);
            }

            var updateCommand = new UpdateUserCommand
            {
                Id = model.Id,
                UserName = model.UserName,
                FullNameEn = model.FullNameEn,
                FullNameAr = model.FullNameAr,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                BirthDate = model.BirthDate,
                Gender = model.Gender
            };

            var result = await Sender.Send(updateCommand);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "حدث خطأ أثناء التحديث");
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return PartialView("_EditPartial", model);
                return View(model);
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new { isSuccess = true });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Sender.Send(new DeleteUserCommand { userId = id });

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }

            TempData["Success"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSelected(string ids)
        {
            var list = ids.Split(',')
                          .Select(Guid.Parse)
                          .ToList();

            foreach (var id in list)
            {
                await Sender.Send(new DeleteUserCommand { userId = id });
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var query = new GetCasesQuery { DesignerId = id };
            var response = await Sender.Send(query);
            var designer = await Sender.Send(new GetUserDataQuery { Id = id });

            if (!response.IsSuccess || designer.Data == null)
                return NotFound();

            var vm = new List<CaseViewModel>();
            foreach (var c in response.Data)
            {
                string designerName = "Not Assigned";
                if (c.DesignertId != null)
                {
                    var user = await Sender.Send(new GetUserDataQuery { Id = c.DesignertId.Value });
                    designerName = user?.Data?.FullNameEn ?? "Unknown";
                }

                vm.Add(new CaseViewModel
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

            ViewBag.DesignerName = designer.Data.FullNameEn;

            
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_DetailsPartial", vm);

            return View(vm); // صفحة كاملة (للوصول المباشر)
        }
    }
}
