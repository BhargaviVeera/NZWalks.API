using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers
{
    [Authorize] // 🔐 all endpoints in this controller require authentication
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private static readonly List<Region> regions = new List<Region>();

        [HttpGet]
        public IActionResult GetAll()
        {
            var dtoList = regions.Select(r => new RegionDTO
            {
                Id = r.Id,
                Code = r.Code,
                Name = r.Name,
                RegionImageUrl = r.RegionImageUrl
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var region = regions.FirstOrDefault(r => r.Id == id);
            if (region == null) return NotFound();

            var dto = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionDTO dto)
        {
            if (dto == null) return BadRequest();

            var region = new Region
            {
                Id = Guid.NewGuid(),
                Code = dto.Code,
                Name = dto.Name,
                RegionImageUrl = dto.RegionImageUrl
            };

            regions.Add(region);

            var responseDto = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = region.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] UpdateRegionDTO dto)
        {
            var existing = regions.FirstOrDefault(r => r.Id == id);
            if (existing == null) return NotFound();

            existing.Code = dto.Code;
            existing.Name = dto.Name;
            existing.RegionImageUrl = dto.RegionImageUrl;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var existing = regions.FirstOrDefault(r => r.Id == id);
            if (existing == null) return NotFound();

            regions.Remove(existing);
            return NoContent();
        }
    }
}
