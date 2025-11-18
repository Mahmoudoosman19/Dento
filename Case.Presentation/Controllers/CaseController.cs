using Case.Application.Features.Case.Command.AddCase;
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
        
    }
}
