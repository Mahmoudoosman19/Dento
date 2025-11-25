using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTOs;
using UserManagement.Application.Features.Supervisor.Queries.GetSupervisorByNameAndStatusAndRoleId;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;
using UserManagement.Infrastructure.Data;

namespace DentalDesign.Dashboard.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly ISender Sender;

        public SupervisorController(ISender sender)
        {
            this.Sender = sender;
        }

        public async Task<IActionResult> Index(GetSupervisorByNameAndStatusAndRoleIdQuery query)
        {
            // نرسل الـ Query للـ Mediator
            var response = await Sender.Send(query);

            if (!response.IsSuccess)
            {
                // لو حصل خطأ، نرجع صفحة فاضية أو رسالة
                return View(new List<UserDto>());
            }

            // نحسب Pagination
            ViewBag.CurrentPage = query.PageIndex;
            ViewBag.PageSize = query.PageSize;
            ViewBag.TotalCount = response.TotalCount;
            ViewBag.TotalPages = (int)Math.Ceiling((double)response.TotalCount! / (double)query.PageSize);
            ViewBag.SearchName = query.Name;
            ViewBag.SearchStatus = query.Status;

            return View(response.Data);
        }

    }
}
