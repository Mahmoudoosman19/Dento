using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Command.AddCase
{
    public class AddCaseCommandValidator : AbstractValidator<AddCaseCommand>
    {
        public AddCaseCommandValidator()
        {
            RuleFor(c => c.CaseName)
                .NotEmpty()
                .WithMessage("Case Name Reqiuerd");
        }
    }
}
