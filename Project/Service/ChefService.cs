using Microsoft.EntityFrameworkCore;
using Restuarant_Management.DTO;
using Restuarant_Management.Interfaces;
using Restuarant_Management.Models;

namespace Restuarant_Management.Service
{
    public class ChefService
    {
        private readonly INamedEntityRepository<Chef> _chefRepo;

        public ChefService(INamedEntityRepository<Chef> chefRepo)
        {
            _chefRepo = chefRepo;
        }
        public async Task<IEnumerable<Chef>> GetAllAsync() => await _chefRepo.GetAllAsync();
        public async Task<Chef?> GetByIdAsync(int id) => await _chefRepo.GetByIdAsync(id);
        public async Task<Chef?> GetByNameAsync(string name) => await _chefRepo.GetByNameAsync(name);

        public async Task AddAsync(ChefDTO dto)
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
            await _chefRepo.AddAsync(chef);
        }

        public async Task UpdateAsync(Chef chef)
        {
            await _chefRepo.UpdateAsync(chef);
            await _chefRepo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _chefRepo.DeleteAsync(id);
            await _chefRepo.SaveAsync();
        }
    }
}
