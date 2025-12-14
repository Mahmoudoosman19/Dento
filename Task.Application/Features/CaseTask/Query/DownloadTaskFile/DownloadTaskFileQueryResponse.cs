using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Application.Features.CaseTask.Query.DownloadTaskFile
{
    internal class DownloadTaskFileQueryResponse
    {
        public byte[] File { get; set; }
        public string FilePath { get; set; }
    }
}
