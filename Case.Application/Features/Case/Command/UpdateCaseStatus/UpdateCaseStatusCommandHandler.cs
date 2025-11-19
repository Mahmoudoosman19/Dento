using Case.Domain.Repositories;
using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Command.UpdateCaseStatus
{
    internal class UpdateCaseStatusCommandHandler : ICommandHandler<UpdateCaseStatusCommand>
    {
        private readonly ICaseUnitOfWork _uow;

        public UpdateCaseStatusCommandHandler(ICaseUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ResponseModel> Handle(UpdateCaseStatusCommand request, CancellationToken cancellationToken)
        {
            var casee = await _uow.Repository<Domain.Entities.Case>().GetByIdAsync(request.CaseId);
            if (casee == null)
                return  ResponseModel.Failure("Case Not Found!");
            casee.SetStatus((Domain.Enum.CaseStatusEnum)request.StatusId);
            await _uow.CompleteAsync();
        
            return ResponseModel.Success();
        }
    }
}
