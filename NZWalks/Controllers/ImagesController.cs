using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var images = await _imageRepository.GetAllAsync();
            return Ok(images);
        }

        // POST: api/Images/ImageUpload
        [HttpPost]
        [Route("ImageUpload")]
        public async Task<IActionResult> ImageUpload([FromForm] ImageUploadDTO imageUpload)
        {
            ValidateFileUpload(imageUpload);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var image = new Image
            {
                FormFile = imageUpload.FormFile,
                FileName = imageUpload.FileName,
                FileDescription = imageUpload.FileDescription,
                FileExtension = Path.GetExtension(imageUpload.FormFile.FileName).Trim(),
                FileSizeInBytes = imageUpload.FormFile.Length
            };

            await _imageRepository.ImageUpload(image);

            return Ok(image);
        }

        private void ValidateFileUpload(ImageUploadDTO imageUpload)
        {
            var extensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

            if (!extensions.Contains(Path.GetExtension(imageUpload.FormFile.FileName)))
            {
                ModelState.AddModelError("file", "Extension not supported!");
            }

            if (imageUpload.FormFile.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("file", "Please upload files smaller than 5MB!");
            }
        }
    }
}
