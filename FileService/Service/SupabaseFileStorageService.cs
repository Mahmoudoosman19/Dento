using FileService.Abstraction;
using Microsoft.Extensions.Configuration;
using Supabase;
using Supabase.Interfaces;
using Supabase.Storage;
using Supabase.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Service
{
    public class SupabaseFileStorageService : IFileStorageService
    {
        private readonly Supabase.Client _client;
        private readonly IStorageClient<Bucket, FileObject> _storage;
        private readonly string _defaultBucket;

        public SupabaseFileStorageService(IConfiguration config)
        {
            var url = config["Supabase:Url"] ?? throw new ArgumentNullException("Supabase:Url");
            var key = config["Supabase:Key"] ?? throw new ArgumentNullException("Supabase:Key");
            _defaultBucket = config["Supabase:DefaultBucket"] ?? "Dento";

            // إنشاء العميل وتهيئته
            _client = new Supabase.Client(url, key);
            // initialize sync (safe on startup)
            _client.InitializeAsync().GetAwaiter().GetResult();

            // الحصول على Storage client (نوع IStorageClient<Bucket,FileObject>)
            _storage = _client.Storage;
        }

        public async Task<string> UploadAsync(string bucket, string path, Stream file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var options = new Supabase.Storage.FileOptions
            {
                Upsert = true
            };

            await _storage
                .From(bucket)
                .Upload(ms.ToArray(), path, options);

            return path;
        }

        public async Task<byte[]> DownloadAsync(string bucket, string path)
        {
            bucket ??= _defaultBucket;
            return await _storage
                .From(bucket)
                .Download(path, (Supabase.Storage.TransformOptions?)null);
        }

        public async Task<bool> DeleteAsync(string bucket, string path)
        {
            bucket ??= _defaultBucket;
            await _storage.From(bucket).Remove(path);
            return true;
        }

        public string GetSignedUrl(string bucket, string path)
        {
            bucket ??= _defaultBucket;
            return _storage.From(bucket).GetPublicUrl(path);
        }
     

        public async Task<string> CreateSignedUrlAsync(string bucket, string path, int expiresInSeconds = 3600)
        {
            bucket ??= _defaultBucket;
            return await _storage.From(bucket).CreateSignedUrl(path, expiresInSeconds);
        }
    }
}
