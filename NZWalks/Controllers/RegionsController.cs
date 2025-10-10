using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;

        public RegionController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllAsync();
            return Ok(regions.Select(r => new RegionDTO
            {
                Id = r.Id,
                Name = r.Name,
                Code = r.Code,
                RegionImageUrl = r.RegionImageUrl
            }));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionDTO dto)
        {
            if (dto == null) return BadRequest();

            var region = new Region
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Code = dto.Code,
                RegionImageUrl = dto.RegionImageUrl
            };

            await regionRepository.CreateAsync(region);

            return CreatedAtAction(nameof(GetAll), new { id = region.Id }, region);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRegionDTO dto)
        {
            var region = new Region
            {
                Name = dto.Name,
                Code = dto.Code,
                RegionImageUrl = dto.RegionImageUrl
            };

            var updated = await regionRepository.UpdateAsync(id, region);

            if (updated == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await regionRepository.DeleteAsync(id);

            if (deleted == null) return NotFound();

            return NoContent();
        }
    }
}
