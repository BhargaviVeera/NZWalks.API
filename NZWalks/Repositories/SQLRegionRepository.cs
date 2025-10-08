using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existing = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existing == null) return null;

            existing.Name = region.Name;
            existing.Code = region.Code;
            existing.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existing = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existing == null) return null;

            dbContext.Regions.Remove(existing);
            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}
