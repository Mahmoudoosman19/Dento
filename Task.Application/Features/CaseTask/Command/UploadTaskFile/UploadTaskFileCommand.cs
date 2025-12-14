using Common.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Application.Features.CaseTask.Command.UploadTaskFile
{
    public class UploadTaskFileCommand : ICommand
    {
        public Guid TaskId { get; set; }
        public IFormFile File { get; set; }

    }
}
