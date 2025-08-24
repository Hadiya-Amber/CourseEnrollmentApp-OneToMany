using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restuarant_Management.DTO;
using Restuarant_Management.Models;
using Restuarant_Management.Service;

namespace Restuarant_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET all users 
        [HttpGet]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            var dtos = users.Select(u => new User
            {
                UserId = u.UserId,
                Username = u.Username,
                Role = u.Role,
                RegisteredDate = u.RegisteredDate
            });

            return Ok(dtos);
        }

        // GET single user 
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager")] 
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(new UserDTO
            {
                Username = user.Username,
                Role = user.Role,
                RegisteredDate = user.RegisteredDate
            });
        }
        // CREATE user
        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Role = dto.Role,
                RegisteredDate = DateTime.UtcNow,
                PasswordHash = "" // Empty → since login handled by TokenService
            };

            await _userService.AddAsync(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")] 
        public async Task<IActionResult> UpdateUser(int id, UserDTO dto)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            user.Username = dto.Username ?? user.Username;
            user.Role = dto.Role ?? user.Role;

            await _userService.UpdateAsync(user);
            return NoContent();
        }
        // DELETE user
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
