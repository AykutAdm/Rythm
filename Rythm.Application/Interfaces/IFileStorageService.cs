using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadAudioAsync(Stream fileStream, string fileName);
        Task<string> UploadImageAsync(Stream fileStream, string fileName);
        Task DeleteFileAsync(string filePath);
    }
}
