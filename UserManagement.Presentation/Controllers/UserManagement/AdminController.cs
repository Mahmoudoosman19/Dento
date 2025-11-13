using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.Auth.Commands.Register;
using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
using UserManagement.Application.Features.Auth.Commands.Register.DTOs;
using UserManagement.Application.Features.Customer.Queries.GetCustomerByRoleId;
using UserManagement.Application.Features.MetaData.Commands;
using UserManagement.Application.Features.Notifications.Command.NotificationMessage;
using UserManagement.Application.Features.Notifications.Queries;
using UserManagement.Application.Features.Notifications.Queries.GetNotificationMessageAndReplace;
using UserManagement.Application.Features.Supervisor.Queries.GetSupervisorByNameAndStatusAndRoleId;
using UserManagement.Application.Features.User.Commands.DeleteUser;
using UserManagement.Application.Features.User.Commands.RestoredUser;
using UserManagement.Application.Features.User.Commands.ToggleAccountActivation;
using UserManagement.Application.Features.User.Queries.GetUserData;
using UserManagement.Application.Features.User.Queries.GetUsersData;
using UserManagement.Application.Features.Designer.Queries.GetListDesigners;
using UserManagement.Application.Features.Designer.Queries.GetTopDesigners;
using UserManagement.Application.Features.User.Commands.SoftDeleteUser;

namespace UserManagement.Presentation.Controllers
{

    [Route("api/UserManagement/[controller]")]
    public sealed class AdminController : ApiController
    {
        public AdminController(ISender sender) : base(sender)
        {

        }
        [HttpPost("AdminRegisterSupervisor")]
        public async Task<IActionResult> AdminRegisterSupervisor([FromForm] SupervisorRegisterDto command)
        {
            var registerCommand = new RegisterCommand(command, RegisterType.Supervisor);
            var result = await Sender.Send(registerCommand);
            return HandleResult(result);
        }
        [HttpPost("Admin-Register-Designer")]
        public async Task<IActionResult> AdminRegisterDesigner([FromForm] DesignerRegisterDto command)
        {
            var registerCommand = new RegisterCommand(command, RegisterType.DesignerByAdmin);
            var result = await Sender.Send(registerCommand);
            return HandleResult(result);
        }

        [HttpGet("get-list-customer")]
        public async Task<IActionResult> GetListCustomer([FromQuery] GetCustomerByRoleIdQuery query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }
        [HttpGet("get-list-suprvisor")]
        public async Task<IActionResult> GetListSupervisor([FromQuery] GetSupervisorByNameAndStatusAndRoleIdQuery query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }

        [HttpGet("get-list-designers")]
        public async Task<IActionResult> GetListDesigners([FromQuery] GetListDesignersQuery request)
        {
            var result = await Sender.Send(request);
            return HandleResult(result);
        }

        [HttpPost("toggle-designer-activation")]
        public async Task<IActionResult> ToggleDesignerActivation([FromBody] ToggleUserAccountActivationCommand request, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(request, cancellationToken);
            return HandleResult(result);
        }

        [HttpPost("toggle-supervisor-activation")]
        public async Task<IActionResult> ToggleSupervisorActivation([FromBody] ToggleUserAccountActivationCommand request, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(request, cancellationToken);
            return HandleResult(result);
        }

        [HttpPost("toggle-customer-activation")]
        public async Task<IActionResult> ToggleCustomerActivation([FromBody] ToggleUserAccountActivationCommand request, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(request, cancellationToken);
            return HandleResult(result);
        }

        [HttpGet("get-all-notifications")]
        public async Task<IActionResult> GetNotifications([FromQuery] GetNotificationsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(request, cancellationToken);
            return HandleResult(result);
        }
        [HttpGet("get-top-designers")]
        public async Task<IActionResult> GetTopDesigners([FromQuery] GetTopDesignersQuery request, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(request, cancellationToken);
            return HandleResult(result);
        }
        [HttpGet("Get-User-Data")]
        public async Task<IActionResult> GetUserData([FromQuery] GetUserDataQuery request, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(request, cancellationToken);
            return HandleResult(result);
        }
        [HttpGet("Get-Users")]
        public async Task<IActionResult> GetUser([FromQuery] List<Guid> ids, CancellationToken cancellationToken)
        {
            var query = new GetUsersDataQuery
            {
                Ids = ids
            };
            var result = await Sender.Send(query, cancellationToken);
            return Ok(result.Data);
        }
        [HttpPost("Update-Meta-Data")]
        public async Task<IActionResult> UpdateMetaData([FromForm] AddOrUpdateMetaDataCommand command)
        {
            if (command == null)
                return BadRequest("Invalid input.");

            var result = await Sender.Send(command);
            return Ok(result);
        }
        [HttpPost("Create-Notification-Message")]
        public async Task<IActionResult> CreateNotificationMessage([FromQuery] AddNotificationMessageCommand query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }
        [HttpGet("Get-Notification-Message")]
        public async Task<IActionResult> GetNotificationMessage([FromQuery] GetNotificationMessageAndReplaceQuery query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }


        [HttpDelete("Delete-User")]
        public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserCommand query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }

        [HttpPut("Soft-Delete-User")]
        public async Task<IActionResult> SoftDeleteUser([FromQuery] SoftDeleteUserCommand query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }

        [HttpPut("Restore-User")]
        public async Task<IActionResult> RestoreUser([FromQuery] RestoredUserCommand query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }
    }
}
