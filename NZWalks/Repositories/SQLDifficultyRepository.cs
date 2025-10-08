using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NZWalks.Repositories
{
    public class SQLDifficultyRepository : IDifficultyRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLDifficultyRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Difficulty>> GetAllAsync()  // ✅ return type matches interface
        {
            return await dbContext.Difficulties.ToListAsync();
        }

        public async Task<Difficulty?> GetByIdAsync(Guid id)
        {
            return await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            await dbContext.Difficulties.AddAsync(difficulty);
            await dbContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<Difficulty?> UpdateAsync(Guid id, Difficulty difficulty)
        {
            var existing = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);

            if (existing == null) return null;

            existing.Name = difficulty.Name;

            await dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<Difficulty?> DeleteAsync(Guid id)
        {
            var existing = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);

            if (existing == null) return null;

            dbContext.Difficulties.Remove(existing);
            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}
