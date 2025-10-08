using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IImageRepository
    {
        Task<Image> ImageUpload(Image image);
    }
}
