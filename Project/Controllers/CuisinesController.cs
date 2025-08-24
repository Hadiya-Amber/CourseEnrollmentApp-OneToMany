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
    public class CuisinesController : ControllerBase
    {
        private readonly CuisineService _cuisineService;

        public CuisinesController(CuisineService cuisineService)
        {
            _cuisineService = cuisineService;
        }

        // ✅ Return DTOs, not Entities
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Cuisine>>> GetCuisines()
        {
            var cuisines = await _cuisineService.GetAllAsync();
            return Ok(cuisines);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<CuisineDTO>> GetCuisine(int id)
        {
            var cuisine = await _cuisineService.GetByIdAsync(id);
            if (cuisine == null) return NotFound();

            return Ok(cuisine);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> PostCuisine([FromBody] CuisineDTO dto)
        {
            if (string.IsNullOrEmpty(dto.CuisineName))
                return BadRequest("CuisineName is required.");
            if (string.IsNullOrEmpty(dto.DishName))
                return BadRequest("DishName is required.");
            if (dto.Price <= 0)
                return BadRequest("Price must be greater than zero.");

            await _cuisineService.AddAsync(dto);

            return Ok(new { message = "Cuisine created successfully" });
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
     
        public async Task<IActionResult> PutCuisine(int id, [FromBody] CuisineDTO dto)
        {
            if (string.IsNullOrEmpty(dto.CuisineName))
                return BadRequest("CuisineName is required.");
            if (string.IsNullOrEmpty(dto.DishName))
                return BadRequest("DishName is required.");
            if (dto.Price <= 0)
                return BadRequest("Price must be greater than zero.");

            var existingCuisine = await _cuisineService.GetByIdAsync(id);
            if (existingCuisine == null)
                return NotFound($"Cuisine with Id {id} not found.");

            await _cuisineService.UpdateAsync(id, dto);
            return Ok(new { message = "Cuisine updated successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCuisine(int id)
        {
            var existingCuisine = await _cuisineService.GetByIdAsync(id);
            if (existingCuisine == null)
                return NotFound($"Cuisine with Id {id} not found.");

            await _cuisineService.DeleteAsync(id);
            return Ok(new { message = "Cuisine deleted successfully" });
        }
    }
}
