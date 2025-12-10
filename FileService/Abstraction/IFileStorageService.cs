using FileService.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Abstraction
{
    public interface IFileStorageService
    {
        /// <summary>
        /// Upload file to a specific bucket and path.
        /// </summary>
        Task<string> UploadAsync(string bucket, string path, Stream stream);

        /// <summary>
        /// Download file as byte[] from storage.
        /// </summary>
        Task<byte[]> DownloadAsync(string bucket, string path);

        /// <summary>
        /// Delete file from storage.
        /// </summary>
        Task<bool> DeleteAsync(string bucket, string path);

        /// <summary>
        /// Returns a public accessible URL (only works if bucket is public).
        /// </summary>
        string GetSignedUrl(string bucket, string path);
    }
}
