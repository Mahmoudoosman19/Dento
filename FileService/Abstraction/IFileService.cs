using FileService.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Abstraction
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile file,FileType type , Guid? folderName = null! ,string? fileName = null!);
    }
}
