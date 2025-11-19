using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Command.AssignCaseToDesigner
{
    public class AssignCaseToDesignerCommand : ICommand
    {
        public Guid CaseId { get; set; }
        public Guid DesignerId { get; set; }
    }
}
