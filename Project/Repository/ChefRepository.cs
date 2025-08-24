using Microsoft.EntityFrameworkCore;
using Restuarant_Management.Data;
using Restuarant_Management.Interfaces;
using Restuarant_Management.Models;
using System;

namespace Restuarant_Management.Repository
{
    public class ChefRepository : INamedEntityRepository<Chef>
    {
        private readonly RestaurantDbContext _context;

        public ChefRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chef>> GetAllAsync()
        {
            return await _context.Chefs
                .Include(c => c.Cuisines)
                .Include(c => c.Restaurant)
                .ToListAsync();
        }

        public async Task<Chef?> GetByIdAsync(int id)
        {
            return await _context.Chefs
                .Include(c => c.Cuisines)
                .Include(c => c.Restaurant)
                .FirstOrDefaultAsync(c => c.ChefId == id);
        }

        public async Task<Chef?> GetByNameAsync(string name)
        {
            return await _context.Chefs
                .Include(c => c.Cuisines)
                .Include(c => c.Restaurant)
                .FirstOrDefaultAsync(c => c.ChefName == name);
        }

        public async Task AddAsync(Chef entity)
        {
            _context.Chefs.Add(entity);
            await _context.SaveChangesAsync();
           
        }

        public async Task UpdateAsync(Chef entity)
        {
            _context.Chefs.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chef = await _context.Chefs.FindAsync(id);
            if (chef != null)
            {
                _context.Chefs.Remove(chef);
                await _context.SaveChangesAsync();
            }
        }
                public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}