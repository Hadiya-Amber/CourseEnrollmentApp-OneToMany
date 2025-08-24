using Microsoft.EntityFrameworkCore;
using Restuarant_Management.Data;
using Restuarant_Management.Interfaces;
using Restuarant_Management.Models;
using System;

namespace Restuarant_Management.Repository
{
    public class CuisineRepository : INamedEntityRepository<Cuisine>
    {
        private readonly RestaurantDbContext _context;

        public CuisineRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cuisine>> GetAllAsync()
        {
            return await _context.Cuisines
                .Include(c => c.Chef)
                .ToListAsync();
        }

        public async Task<Cuisine?> GetByIdAsync(int id)
        {
            return await _context.Cuisines
                .Include(c => c.Chef)
                .FirstOrDefaultAsync(c => c.CuisineId == id);
        }

        public async Task<Cuisine?> GetByNameAsync(string name)
        {
            return await _context.Cuisines
                .Include(c => c.Chef)
                .FirstOrDefaultAsync(c => c.CuisineName == name);
        }

        public async Task AddAsync(Cuisine entity)
        {
            _context.Cuisines.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cuisine entity)
        {
            _context.Cuisines.Update(entity);
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
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
