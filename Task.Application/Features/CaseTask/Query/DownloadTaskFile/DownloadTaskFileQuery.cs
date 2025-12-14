using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Application.Features.CaseTask.Query.DownloadTaskFile
{
    public class DownloadTaskFileQuery : IQuery<byte[]>
    {
        public Guid TaskId { get; set; }
       

    }
}
