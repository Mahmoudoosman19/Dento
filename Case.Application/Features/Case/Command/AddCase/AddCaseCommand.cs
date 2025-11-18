using Case.Domain.Enum;
using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Command.AddCase
{
    public class AddCaseCommand : ICommand
    {
        [Required(ErrorMessage = "Name Required")]
        public string CaseName { get; set; }
        [Required(ErrorMessage = "Date Required")]
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Type Required")]
        public CaseTypeEnum CaseType { get; set; }
    }
}
