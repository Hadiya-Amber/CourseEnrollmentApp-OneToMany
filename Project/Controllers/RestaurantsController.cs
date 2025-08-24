using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restuarant_Management.DTO;
using Restuarant_Management.Models;
using Restuarant_Management.Service;

namespace Restuarant_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // ✅ Protect all endpoints by default
    public class RestaurantsController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantsController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        // GET 
        [HttpGet]
        [AllowAnonymous] 
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _restaurantService.GetAllModelAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantService.GetModelByIdAsync(id);
            if (restaurant == null)
                return NotFound();

            return Ok(restaurant);
        }

        [HttpGet("byname/{name}")]
        [AllowAnonymous]
        public async Task<ActionResult<Restaurant>> GetRestaurantByName(string name)
        {
            var restaurant = await _restaurantService.GetModelByNameAsync(name);
            if (restaurant == null)
                return NotFound();

            return Ok(restaurant);
        }

        // SEARCH
        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Restaurant>>> SearchRestaurants(
            [FromQuery] string? location,
            [FromQuery] double? minRating)
        {
            var results = await _restaurantService.SearchAsync(location, minRating);
            return Ok(results);
        }

        // POST 
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostRestaurant([FromBody] RestaurantDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _restaurantService.AddAsync(dto);
            return Ok(new { message = "Restaurant created successfully" });
        }

        // PUT
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] RestaurantDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _restaurantService.UpdateAsync(id, dto);

            return Ok(new { message = "Restaurant updated successfully" });
        }

        // DELETE (Admin Only)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            await _restaurantService.DeleteAsync(id);
            return Ok(new { message = "Restaurant deleted successfully" });
        }
    }
}
