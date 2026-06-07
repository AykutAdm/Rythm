using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Infrastructure.Storage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _basePath;

        public LocalFileStorageService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _basePath = Path.Combine(environment.ContentRootPath, configuration["StorageSettings:BasePath"]!);
        }

        public Task DeleteFileAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Task.CompletedTask;
        }

        public async Task<string> UploadAudioAsync(Stream fileStream, string fileName)
        {
            var folder = Path.Combine(_basePath, "songs");
            Directory.CreateDirectory(folder);

            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
            var filePath = Path.Combine(folder, newFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await fileStream.CopyToAsync(stream);

            return $"Storage/songs/{newFileName}";
        }

        public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
        {
            var folder = Path.Combine(_basePath, "images");
            Directory.CreateDirectory(folder);

            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
            var filePath = Path.Combine(folder, newFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await fileStream.CopyToAsync(stream);

            return $"Storage/images/{newFileName}";
        }
    }
}
