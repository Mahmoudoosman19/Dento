using Case.Application.Features.Case.Command.AssignCaseToDesigner;
using Case.Application.Features.Case.Query.GetCases;
using DentalDesign.Dashboard.Models.Case;
using DentalDesign.Dashboard.Models.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTOs;
using UserManagement.Application.Features.Auth.Commands.Register;
using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
using UserManagement.Application.Features.Auth.Commands.Register.DTOs;
using UserManagement.Application.Features.Customer.Queries.GetCustomerByRoleId;
using UserManagement.Application.Features.Designer.Queries.GetDesignerById;
using UserManagement.Application.Features.Designer.Queries.GetListDesigners;
using UserManagement.Application.Features.User.Commands.DeleteUser;
using UserManagement.Application.Features.User.Commands.UpdateUser;
using UserManagement.Application.Features.User.Queries.GetUserData;

namespace DentalDesign.Dashboard.Controllers
{
    public class CustomerController : BaseController
    {

        public CustomerController(ISender sender): base(sender) 
        {
        }

        public async Task<IActionResult> Index(GetCustomerByRoleIdQuery query)
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
                return PartialView("_IndexPartial", response.Data);

            // لو الطلب عادي → رجّع صفحة كاملة بالـ Layout
            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

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
                return View(model);
            }

            return RedirectToAction("Index"); // ارجع للـ List بعد الإنشاء
        }


        // GET: /Customer/Edit/{id}
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

            return View(model);
        }

        // POST: /Customer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


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
                return View(model);
            }

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
            var query = new GetCasesQuery { CustomerId = id };
            var response = await Sender.Send(query);

            var customer = await Sender.Send(new GetUserDataQuery { Id = id });

            if (!response.IsSuccess)
                return NotFound(response.Message);

            var vm = new List<CaseViewModel>();

            foreach (var c in response.Data)
            {
                string designerName = "Not Assigned";

                if (c.DesignertId != null)
                {
                    var user = await Sender.Send(
                        new GetUserDataQuery { Id = c.DesignertId.Value });

                    designerName = user?.Data.FullNameEn ?? "Unknown";
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

            ViewBag.CustomerName = customer.Data.FullNameEn;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_DetailsPartial", vm);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AssignCase(AssignCaseToDesignerCommand command)
        {
            var response = await Sender.Send(command);

            if (!response.IsSuccess)
            {
                return Json(new { isSuccess = false, message = response.Message });
            }

            
            string designerName = "Unknown";

            return Json(new
            {
                isSuccess = true,
                message = "Case Assigned Successfully",
                designerName = designerName 
            });
        }


        public async Task<IActionResult> GetDesigners()
        {
            var response = await Sender.Send(new GetListDesignersQuery());

            if (!response.IsSuccess)
                return Json(new { });

            var result = response.Data.Select(d => new
            {
                id = d.Id,
                fullName = d.FullNameEn
            });

            return Json(result);
        }

    }
}
