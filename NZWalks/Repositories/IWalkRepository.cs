using NZWalks.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NZWalks.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();  // ✅ use IEnumerable
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
