using Microsoft.AspNetCore.Http;
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
        //POST: api/Images/ImageUpload
        [HttpPost]
        [Route("ImageUpload")]
        public async Task<IActionResult> ImageUpload([FromForm] ImageUploadDTO imageupload)
        {
            ValidateFileUpload(imageupload);

            if (ModelState.IsValid)
            {
                //Pass DTO to Domain model
                var image = new Image
                {
                    FormFile = imageupload.FormFile,
                    FileName = imageupload.FileName,
                    FileDescription = imageupload.FileDescription,

                    FileExtension = Path.GetExtension(imageupload.FormFile.FileName).Trim(),
                    FileSizeInBytes = imageupload.FormFile.Length
                };

                //Use Repository to upload image
                await _imageRepository.ImageUpload(image);

                return Ok(image);
            }

            return BadRequest(ModelState);
        }
        
        private void ValidateFileUpload(ImageUploadDTO imageupload)
        {
            var extensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

            if (!extensions.Contains(Path.GetExtension(imageupload.FormFile.FileName)))
            {
                ModelState.AddModelError("file", "Extension not supported!");
            }

            if (imageupload.FormFile.Length > 5242880)
            {
                ModelState.AddModelError("file", "Please upload files smaller than 5MB!");
            }
        }
    }
}
