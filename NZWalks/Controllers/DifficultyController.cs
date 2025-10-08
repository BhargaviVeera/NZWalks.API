using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers
{
    [Authorize] // 🔐 all endpoints in this controller require authentication
    [ApiController]
    [Route("api/[controller]")]
    public class DifficultyController : ControllerBase
    {
        private static readonly List<Difficulty> difficulties = new List<Difficulty>();

        [HttpGet]
        public IActionResult GetAll()
        {
            var dtoList = difficulties.Select(d => new DifficultyDTO
            {
                Id = d.Id,
                Name = d.Name
            }).ToList();

            return Ok(dtoList);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddDifficultyDTO dto)
        {
            if (dto == null) return BadRequest();

            var difficulty = new Difficulty
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            difficulties.Add(difficulty);

            var responseDto = new DifficultyDTO
            {
                Id = difficulty.Id,
                Name = difficulty.Name
            };

            return CreatedAtAction(nameof(GetAll), new { id = difficulty.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] UpdateDifficultyDTO dto)
        {
            var existing = difficulties.FirstOrDefault(d => d.Id == id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var existing = difficulties.FirstOrDefault(d => d.Id == id);
            if (existing == null) return NotFound();

            difficulties.Remove(existing);
            return NoContent();
        }
    }
}
