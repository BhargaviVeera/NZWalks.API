using NZWalks.API.Data;
using NZWalks.Models.Domain;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NZWalks.Repositories;


namespace NZWalks.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NZWalksDbContext _dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<Image> ImageUpload(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            using var imagestream = new FileStream(localFilePath, FileMode.Create);
            await image.FormFile.CopyToAsync(imagestream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            await _dbContext.Images.AddAsync(image);
            await _dbContext.SaveChangesAsync();

            return image;
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            return await _dbContext.Images.ToListAsync(); // <- now works
        }
    }
}
