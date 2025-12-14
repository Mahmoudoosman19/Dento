using Azure.Core;
using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Task.Application.Features.CaseTask.Command.AssignTask;
using Task.Application.Features.CaseTask.Command.CreateTask;
using Task.Application.Features.CaseTask.Command.EndTask;
using Task.Application.Features.CaseTask.Command.StartTask;
using Task.Application.Features.CaseTask.Command.UploadTaskFile;
using Task.Application.Features.CaseTask.Query.DownloadTaskFile;
using Task.Application.Features.CaseTask.Query.GetTaskById;
using Task.Application.Features.CaseTask.Query.GetTasks;

namespace Task.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : ApiController
    {
        public TaskController(ISender sender): base(sender)
        {
            
        }


        [HttpPost("Add-Task")]
        public async Task<IActionResult> AddTask([FromBody]CreateTaskCommand command)
        {
            var response = await Sender.Send(command);
            return HandleResult(response);
        }

        [HttpPost("start-task")]
        public async Task<IActionResult> StartTask([FromBody]StartTaskCommand command)
        {
            var response = await Sender.Send(command);
            return HandleResult(response);
        }

        [HttpPost("end-task")]
        public async Task<IActionResult> EndTask([FromForm]EndTaskCommand command)
        {
            var response = await Sender.Send(command);
            return HandleResult(response);
        }

        [HttpPost("Assign-Task")]
        public async Task<IActionResult> AssignTask([FromForm]AssignTaskCommand command)
        {
            var response = await Sender.Send(command);
            return HandleResult(response);
        }

        [HttpGet("get-task")]
        public async Task<IActionResult> GetTask([FromQuery]GetTaskByIdQuery query)
        {
            var response = await Sender.Send(query);
            return HandleResult(response);
        }

        [HttpGet("GetTasks")]
        public async Task <IActionResult> GetTasks([FromQuery] GetTasksQuery query)
        {
            var response = await Sender.Send(query);
            return HandleResult(response);
        }


        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadTaskFileCommand command)
        {
            var response = await Sender.Send(command);
            return HandleResult(response);
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download([FromQuery]DownloadTaskFileQuery query)
        {
            var response = await Sender.Send(query);
            return File(response.Data, "application/octet-stream","task.stl");
        }
    }
}
