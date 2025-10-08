using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class ImageUploadDTO
    {
        [Required]
        public IFormFile FormFile { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
