using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using FileService.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Repository_Abstraction;

namespace Task.Application.Features.CaseTask.Query.DownloadTaskFile
{
    internal class DownloadTaskFileQueryHandler : IQueryHandler<DownloadTaskFileQuery, byte[]>
    {
        private readonly ITaskUnitOfWork _uow;
        private readonly IFileStorageService _storage;

        public DownloadTaskFileQueryHandler(ITaskUnitOfWork uow,IFileStorageService storage)
        {
            _uow = uow;
            _storage = storage;
        }
        public async Task<ResponseModel<byte[]>> Handle(DownloadTaskFileQuery request, CancellationToken cancellationToken)
        {
            var task = _uow.Repository<Domain.Entities.CaseTask>().GetByIdAsync(request.TaskId);
            if (task == null)
                return ResponseModel.Failure<byte[]>("Task Not Found");

            if(task.Result!.Model3DPath == null)
                return ResponseModel.Failure<byte[]>("No File exists!");
           
            return await _storage.DownloadAsync("Dento", task.Result.Model3DPath);
        }
    }
}
