using Microsoft.EntityFrameworkCore;
using Restuarant_Management.Data;
using Restuarant_Management.DTO;
using Restuarant_Management.Interfaces;
using Restuarant_Management.Models;
using System;

namespace Restuarant_Management.Repository
{
    public class RestaurantRepository : INamedEntityRepository<Restaurant>
    {
        private readonly RestaurantDbContext _context;

        public RestaurantRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants
                .Include(r => r.Chefs)  
                .ToListAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            return await _context.Restaurants
                .Include(r => r.Chefs)
                .FirstOrDefaultAsync(r => r.RestaurantId == id);
        }

        public async Task<Restaurant?> GetByNameAsync(string name)
        {
            return await _context.Restaurants
                .Include(r => r.Chefs)
                .FirstOrDefaultAsync(r => r.RestuarantName == name);
        }

        public async Task AddAsync(Restaurant entity)
        {
            await _context.Restaurants.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Restaurant entity)
        {
            _context.Restaurants.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
