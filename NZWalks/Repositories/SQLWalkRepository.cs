using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NZWalks.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()  // ✅ return type matches interface
        {
            return await dbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existing = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return null;

            existing.Name = walk.Name;
            existing.Description = walk.Description;
            existing.LengthInKm = walk.LengthInKm;
            existing.WalkImageUrl = walk.WalkImageUrl;
            existing.RegionId = walk.RegionId;
            existing.DifficultyId = walk.DifficultyId;

            await dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existing = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return null;

            dbContext.Walks.Remove(existing);
            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}
