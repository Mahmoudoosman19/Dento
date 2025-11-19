using Case.Application.Features.Case.Command.AddCase;
using Case.Application.Features.Case.Command.AssignCaseToDesigner;
using Case.Application.Features.Case.Command.UpdateCaseStatus;
using Case.Application.Features.Case.Query.GetCaseById;
using Case.Application.Features.Case.Query.GetCases;
using Case.Application.Features.Case.Query.GetCasesAssignedToDesigner;
using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class CaseController : ApiController
    {
        public CaseController(ISender sender) : base(sender)
        {
            
        }

        [HttpPost("create-case")]
        public async Task<IActionResult> CreateCase([FromBody] AddCaseCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }
        [HttpPut("Assign-Case")]
        public async Task<IActionResult> AssignCase([FromForm] AssignCaseToDesignerCommand command)
        {
            var result =await Sender.Send(command);
            return HandleResult(result);
        }

        [HttpGet("Get-Case")]
        public async Task<IActionResult> GetCase([FromBody] GetCaseByIdQuery query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }
        [HttpGet("Get-All-Cases")]
        public async Task<IActionResult> GetAllCases([FromQuery]GetCasesQuery query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }
        [HttpGet("Get-Designer-Cases")]
        public async Task<IActionResult> GetDesignerCases([FromQuery] GetCasesAssignedToDesignerQuery query)
        {
            var result = await Sender.Send(query);
            return HandleResult(result);
        }
        [HttpPut("updateStatus")]
        public async Task<IActionResult> UpdateStatus([FromForm] UpdateCaseStatusCommand request)
        {
            var result = await Sender.Send(request);
            return HandleResult(result);
        }
        
    }
}
