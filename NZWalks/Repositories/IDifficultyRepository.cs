using NZWalks.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NZWalks.Repositories
{
    public interface IDifficultyRepository
    {
        Task<IEnumerable<Difficulty>> GetAllAsync();  // ✅ use IEnumerable
        Task<Difficulty?> GetByIdAsync(Guid id);
        Task<Difficulty> CreateAsync(Difficulty difficulty);
        Task<Difficulty?> UpdateAsync(Guid id, Difficulty difficulty);
        Task<Difficulty?> DeleteAsync(Guid id);
    }
}
