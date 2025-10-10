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
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;

        public WalkController(IWalkRepository walkRepository)
        {
            this.walkRepository = walkRepository;
        }

        // GET: api/walk
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await walkRepository.GetAllAsync();
            return Ok(walks);
        }

        // GET: api/walk/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            if (walk == null) return NotFound();
            return Ok(walk);
        }

        // POST: api/walk
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkDTO dto)
        {
            if (dto == null) return BadRequest();

            var walk = new Walk
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                LengthInKm = dto.LengthInKm,
                RegionId = dto.RegionId,
                DifficultyId = dto.DifficultyId
            };

            var createdWalk = await walkRepository.CreateAsync(walk);

            return CreatedAtAction(nameof(GetById), new { id = createdWalk.Id }, createdWalk);
        }

        // PUT: api/walk/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AddWalkDTO dto)
        {
            if (dto == null) return BadRequest();

            var walk = new Walk
            {
                Name = dto.Name,
                Description = dto.Description,
                LengthInKm = dto.LengthInKm,
                RegionId = dto.RegionId,
                DifficultyId = dto.DifficultyId
            };

            var updatedWalk = await walkRepository.UpdateAsync(id, walk);

            if (updatedWalk == null) return NotFound();

            return NoContent();
        }

        // DELETE: api/walk/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedWalk = await walkRepository.DeleteAsync(id);

            if (deletedWalk == null) return NotFound();

            return NoContent();
        }
    }
}
