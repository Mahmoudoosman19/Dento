using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using FileService.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Query.DownloadCaseFileQuery
{
    internal class DownloadCaseFileQueryHandler : IQueryHandler<DownloadCaseFileQuery, byte[]>
    {
        private readonly IFileStorageService _storage;

        public DownloadCaseFileQueryHandler(IFileStorageService storage)
        {
            _storage = storage;
        }

        public async Task<ResponseModel<byte[]>> Handle(DownloadCaseFileQuery request, CancellationToken cancellationToken)
        {
            return await _storage.DownloadAsync("Dento", request.FilePath);

        }
    }
}
