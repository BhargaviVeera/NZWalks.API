using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [Authorize] // 🔐 all endpoints in this controller require authentication
    [ApiController]
    [Route("api/[controller]")]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await walkRepository.GetAllAsync();
            return Ok(mapper.Map<List<WalkDTO>>(walks));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            if (walk == null) return NotFound();
            return Ok(mapper.Map<WalkDTO>(walk));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkDTO dto)
        {
            var walk = mapper.Map<Walk>(dto);
            walk = await walkRepository.CreateAsync(walk);
            return CreatedAtAction(nameof(GetById), new { id = walk.Id }, mapper.Map<WalkDTO>(walk));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWalkDTO dto)
        {
            var walk = mapper.Map<Walk>(dto);
            var updatedWalk = await walkRepository.UpdateAsync(id, walk);
            if (updatedWalk == null) return NotFound();
            return Ok(mapper.Map<WalkDTO>(updatedWalk));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedWalk = await walkRepository.DeleteAsync(id);
            if (deletedWalk == null) return NotFound();
            return Ok(mapper.Map<WalkDTO>(deletedWalk));
        }
    }
}
