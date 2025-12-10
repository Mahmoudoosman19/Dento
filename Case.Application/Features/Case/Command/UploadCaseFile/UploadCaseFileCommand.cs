using Common.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Command.UploadCaseFile
{
    public class UploadCaseFileCommand: ICommand<string>
    {
        public Guid CaseId { get; set; }
        public IFormFile File { get; set; }
    }
}
