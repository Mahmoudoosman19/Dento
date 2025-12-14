using Case.Domain.Repositories;
using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using FileService.Abstraction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Case.Application.Features.Case.Command.UploadCaseFile
{
    internal class UploadCaseFileCommandHandler : ICommandHandler<UploadCaseFileCommand, string>
    {
        private readonly IFileStorageService _storage;
        private readonly ICaseUnitOfWork _uow; 

        public UploadCaseFileCommandHandler(IFileStorageService storage, ICaseUnitOfWork uow)
        {
            _storage = storage;
            _uow = uow;
        }
        public async Task<ResponseModel<string>> Handle(UploadCaseFileCommand request, CancellationToken cancellationToken)
        {
            var caseEntity = await _uow.Repository<Domain.Entities.Case>().GetByIdAsync(request.CaseId);
            if (caseEntity == null) 
                return ResponseModel.Failure<string>("Case not found");

            // إنشاء path فريد لكل ملف
            var filePath = $"cases/{request.CaseId}/{Guid.NewGuid()}_{request.File.FileName}";
            
            // رفع الملف
            using var stream = request.File.OpenReadStream();
            await _storage.UploadAsync("Dento", filePath, stream);

            // ربط الملف بالـ Case
            caseEntity.SetModel3DPath(filePath); 
             await _uow.CompleteAsync();

            return ResponseModel.Success(filePath);
        }
    }
}
