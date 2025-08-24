using Microsoft.AspNetCore.Mvc;
using Restuarant_Management.DTO;
using Restuarant_Management.Interfaces;
using Restuarant_Management.Models;
using Restuarant_Management.Repository;

namespace Restuarant_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IToken _tokenService;

        public TokenController(IRepository<User> userRepository, IToken tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Username == dto.Username);

            if (user == null)
                return Unauthorized("Invalid username");

            // ⚡ You should verify password here (hash check)

            string token = _tokenService.GenerateToken(
                user.UserId.ToString(),
                user.Username!,
                user.Role!
            );

            return Ok(new { Token = token });
        }
    }
}
