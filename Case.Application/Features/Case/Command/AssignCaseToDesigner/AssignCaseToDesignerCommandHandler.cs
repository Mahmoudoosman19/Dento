using Case.Domain.Repositories;
using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserManagement.Application.Identity;

namespace Case.Application.Features.Case.Command.AssignCaseToDesigner
{
    internal class AssignCaseToDesignerCommandHandler : ICommandHandler<AssignCaseToDesignerCommand>
    {
        private readonly ICaseUnitOfWork _uow;

        public AssignCaseToDesignerCommandHandler(ICaseUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ResponseModel> Handle(AssignCaseToDesignerCommand request, CancellationToken cancellationToken)
        {
            var caseToAssign= await _uow.Repository<Domain.Entities.Case>().GetByIdAsync(request.CaseId);
            
            if (caseToAssign == null)
                return ResponseModel.Failure("Case Not Exist!");

            caseToAssign.SetDesignertId(request.DesignerId);
            caseToAssign.SetAssignedAt(DateTime.Now);
            await _uow.CompleteAsync();

            return ResponseModel.Success();
        }
    }
}
