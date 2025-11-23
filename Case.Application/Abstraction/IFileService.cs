using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Abstraction
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile file);
    }
}
