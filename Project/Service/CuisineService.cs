using Microsoft.EntityFrameworkCore;
using Restuarant_Management.Data;
using Restuarant_Management.DTO;
using Restuarant_Management.Models;

namespace Restuarant_Management.Service
{
    public class CuisineService
    {
        private readonly RestaurantDbContext _context;

        public CuisineService(RestaurantDbContext context)
        {
            _context = context;
        }

        // ✅ Return entity directly for GET
        public async Task<IEnumerable<Cuisine>> GetAllAsync()
        {
            return await _context.Cuisines.ToListAsync();
        }

        public async Task<Cuisine?> GetByIdAsync(int id)
        {
            return await _context.Cuisines.FindAsync(id);
        }

        // ✅ Use DTO for POST
        public async Task AddAsync(CuisineDTO dto)
        {
            var cuisine = new Cuisine
            {
                CuisineName = dto.CuisineName,
                DishName = dto.DishName,
                Price = dto.Price,
                ChefId = dto.ChefId,
                IsVegetarian = dto.IsVegetarian
            };

            _context.Cuisines.Add(cuisine);
            await _context.SaveChangesAsync();
        }

        // ✅ Use DTO for PUT
        public async Task UpdateAsync(int id, CuisineDTO dto)
        {
            var cuisine = await _context.Cuisines.FindAsync(id);
            if (cuisine == null) return;

            cuisine.CuisineName = dto.CuisineName;
            cuisine.DishName = dto.DishName;
            cuisine.Price = dto.Price;
            cuisine.ChefId = dto.ChefId;
            cuisine.IsVegetarian = dto.IsVegetarian;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cuisine = await _context.Cuisines.FindAsync(id);
            if (cuisine != null)
            {
                _context.Cuisines.Remove(cuisine);
                await _context.SaveChangesAsync();
            }
        }
    }
}
