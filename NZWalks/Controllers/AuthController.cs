using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Username
            };

            var result = await _userManager.CreateAsync(identityUser, registerDTO.Password);

            if (result.Succeeded)
            {
                if (registerDTO.Roles != null && registerDTO.Roles.Any())
                {
                    result = await _userManager.AddToRolesAsync(identityUser, registerDTO.Roles);
                    if (result.Succeeded)
                        return Ok("User registered. You can login with new user.");
                }

                return Ok("User registered (no roles assigned).");
            }

            return BadRequest(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Username); // ✅ fixed
            if (user != null)
            {
                var loginStatus = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (loginStatus)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                    return Ok(new LoginRespDTO { JwtToken = jwtToken });
                }
            }

            return BadRequest("Invalid username or password");
        }
    }
}
