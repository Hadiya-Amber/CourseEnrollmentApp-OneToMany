using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restuarant_Management.DTO;
using Restuarant_Management.Models;
using Restuarant_Management.Service;

namespace Restuarant_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 🔒 Entire controller requires JWT
    public class ChefsController : ControllerBase
    {
        private readonly ChefService _chefService;

        public ChefsController(ChefService chefService)
        {
            _chefService = chefService;
        }

        // GET: api/Chefs
        [HttpGet]
        [AllowAnonymous] // 👈 Anyone can see chefs (no token required)
        public async Task<ActionResult<IEnumerable<Chef>>> GetChefs()
        {
            var chefs = await _chefService.GetAllAsync();

            var chef = chefs.Select(c => new Chef
            {
                ChefId = c.ChefId,
                ChefName = c.ChefName,
                ExperienceYears = c.ExperienceYears,
                JoinedDate = c.JoinedDate,
                Salary = c.Salary,
                Specialty = c.Specialty,
                RestaurantId = c.RestaurantId
            });

            return Ok(chef);
        }

        // GET: api/Chefs/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager")] // 👈 Only Admins/Managers can get details
        public async Task<ActionResult<ChefDTO>> GetChef(int id)
        {
            var chef = await _chefService.GetByIdAsync(id);
            if (chef == null) return NotFound();

            return new ChefDTO
            {
                ChefName = chef.ChefName,
                Specialty = chef.Specialty,
                RestaurantId = chef.RestaurantId
            };
        }

        // POST: api/Chefs
        [HttpPost]
        [Authorize(Roles = "Admin")] // 👈 Only Admin can create
        public async Task<IActionResult> CreateChef(ChefDTO dto)
        {
            var chef = new Chef
            {
                ChefName = dto.ChefName,
                ExperienceYears = dto.ExperienceYears,
                JoinedDate = dto.JoinedDate,
                Salary = dto.Salary,
                Specialty = dto.Specialty,
                RestaurantId = dto.RestaurantId
            };

            await _chefService.AddAsync(dto);
            return CreatedAtAction(nameof(GetChef), new { id = chef.ChefId }, chef);
        }

        // PUT: api/Chefs/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")] // 👈 Only Admin/Manager can update
        public async Task<IActionResult> UpdateChef(int id, ChefDTO dto)
        {
            var chef = await _chefService.GetByIdAsync(id);
            if (chef == null)
                return NotFound();

            chef.ChefName = dto.ChefName;
            chef.ExperienceYears = dto.ExperienceYears;
            chef.Salary = dto.Salary;
            chef.Specialty = dto.Specialty;
            chef.RestaurantId = dto.RestaurantId;
            chef.JoinedDate = dto.JoinedDate;

            await _chefService.UpdateAsync(chef);
            return NoContent();
        }

        // DELETE: api/Chefs/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // 👈 Only Admin can delete
        public async Task<IActionResult> DeleteChef(int id)
        {
            await _chefService.DeleteAsync(id);
            return Ok(new { message = "Chef deleted successfully" });
        }
    }
}
