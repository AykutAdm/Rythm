using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rythm.Application.Interfaces;

namespace Rythm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;

        public FileController(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }


        [HttpPost("upload-audio")]
        public async Task<IActionResult> UploadAudio(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Dosya boş olamaz.");
            }

            var allowedExtensions = new[] { ".mp3", ".wav", ".flac" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Geçersiz dosya formatı. mp3, wav veya flac olmalı.");
            }

            var url = await _fileStorageService.UploadAudioAsync(file.OpenReadStream(), file.FileName);
            return Ok(new { url });
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Dosya boş olamaz.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Geçersiz dosya formatı. jpg, jpeg, png veya webp olmalı.");
            }

            var url = await _fileStorageService.UploadImageAsync(file.OpenReadStream(), file.FileName);
            return Ok(new { url });
        }
    }
}
