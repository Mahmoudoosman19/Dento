using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Command.UpdateCaseStatus
{
    public class UpdateCaseStatusCommand : ICommand
    {
        public Guid CaseId { get; set; }
        public long StatusId { get; set; }

    }
}
