using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Query.DownloadCaseFileQuery
{
    public class DownloadCaseFileQuery : IQuery<byte[]>
    {
        public string FilePath { get; set; }
    }
}
