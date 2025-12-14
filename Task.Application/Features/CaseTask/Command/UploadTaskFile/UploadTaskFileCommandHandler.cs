using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using FileService.Abstraction;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Repository_Abstraction;

namespace Task.Application.Features.CaseTask.Command.UploadTaskFile
{
    internal class UploadTaskFileCommandHandler : ICommandHandler<UploadTaskFileCommand>
    {
        private readonly ITaskUnitOfWork _uow;
        private readonly IFileStorageService _fileService;

        public UploadTaskFileCommandHandler(ITaskUnitOfWork uow, IFileStorageService fileService)
        {
            _uow = uow;
            _fileService = fileService;
        }
        public async Task<ResponseModel> Handle(UploadTaskFileCommand request, CancellationToken cancellationToken)
        {
            var task = _uow.Repository<Domain.Entities.CaseTask>().GetByIdAsync(request.TaskId);
            if (task == null)
                return ResponseModel.Failure("Task Not Found!");

            var filePath = $"tasks/{request.TaskId}/{Guid.NewGuid()}_{request.File.FileName}";

            using var stream = request.File.OpenReadStream();
            await _fileService.UploadAsync("Dento", filePath, stream);

            task.Result!.SetModel3DPath(filePath);

            await _uow.CompleteAsync();
            return ResponseModel.Success();
        }
    }
}
