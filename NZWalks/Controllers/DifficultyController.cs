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
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyRepository difficultyRepository;

        public DifficultyController(IDifficultyRepository difficultyRepository)
        {
            this.difficultyRepository = difficultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var difficulties = await difficultyRepository.GetAllAsync();

            var dtoList = difficulties.Select(d => new DifficultyDTO
            {
                Id = d.Id,
                Name = d.Name
            }).ToList();

            return Ok(dtoList);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddDifficultyDTO dto)
        {
            if (dto == null) return BadRequest();

            var difficulty = new Difficulty
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            await difficultyRepository.CreateAsync(difficulty);

            var responseDto = new DifficultyDTO
            {
                Id = difficulty.Id,
                Name = difficulty.Name
            };

            return CreatedAtAction(nameof(GetAll), new { id = difficulty.Id }, responseDto);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDifficultyDTO dto)
        {
            if (dto == null) return BadRequest();

            var difficulty = new Difficulty
            {
                Name = dto.Name
            };

            var updated = await difficultyRepository.UpdateAsync(id, difficulty);

            if (updated == null) return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await difficultyRepository.DeleteAsync(id);

            if (deleted == null) return NotFound();

            return NoContent();
        }

    }
}
