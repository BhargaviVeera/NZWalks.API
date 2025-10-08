using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class InMemoryRegionRepository : IRegionRepository
    {
        private readonly List<Region> regions;

        public InMemoryRegionRepository()
        {
            regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "NZ-N",
                    Name = "North Island",
                    RegionImageUrl = "https://example.com/north.jpg"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "NZ-S",
                    Name = "South Island",
                    RegionImageUrl = "https://example.com/south.jpg"
                }
            };
        }

        public Task<IEnumerable<Region>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Region>>(regions);
        }

        public Task<Region?> GetByIdAsync(Guid id)
        {
            var region = regions.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(region);
        }

        public Task<Region> CreateAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            regions.Add(region);
            return Task.FromResult(region);
        }

        public Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existing = regions.FirstOrDefault(x => x.Id == id);

            if (existing == null) return Task.FromResult<Region?>(null);

            existing.Name = region.Name;
            existing.Code = region.Code;
            existing.RegionImageUrl = region.RegionImageUrl;

            return Task.FromResult(existing);
        }

        public Task<Region?> DeleteAsync(Guid id)
        {
            var existing = regions.FirstOrDefault(x => x.Id == id);

            if (existing == null) return Task.FromResult<Region?>(null);

            regions.Remove(existing);
            return Task.FromResult(existing);
        }
    }
}
