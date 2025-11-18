using Case.Domain.Entities;
using Case.Domain.Repositories;
using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Command.AddCase
{
    internal class AddCaseCommandHandler : ICommandHandler<AddCaseCommand>
    {
        private readonly ICaseUnitOfWork _unitOfWork;
        private readonly ITokenExtractor _tokenExtractor;

        public AddCaseCommandHandler(ICaseUnitOfWork unitOfWork, ITokenExtractor tokenExtractor)
        {
            _unitOfWork = unitOfWork;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel> Handle(AddCaseCommand request, CancellationToken cancellationToken)
        {
            var customerId = _tokenExtractor.GetUserId();
            var newCase = new Domain.Entities.Case();

            newCase.SetStatus(Domain.Enum.CaseStatusEnum.New);
            newCase.SetCaseType(request.CaseType);
            newCase.SetDescription(request.Description);
            newCase.SetCaseName(request.CaseName);
            newCase.SetDueDate(request.DueDate);
            newCase.SetCustomerId(customerId);

            //var caseMapped = _mapper.Map<Domain.Entities.Case>(request);
            await _unitOfWork.Repository<Domain.Entities.Case>().AddAsync(newCase, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return ResponseModel.Success();
        }
    }
}
