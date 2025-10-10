using NZWalks.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NZWalks.Repositories
{
    public interface IImageRepository
    {
        Task<Image> ImageUpload(Image image);          // Upload image
        Task<IEnumerable<Image>> GetAllAsync();       // Get all images
    }
}
