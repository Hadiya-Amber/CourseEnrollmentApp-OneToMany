using Restuarant_Management.DTO;
using Restuarant_Management.Interfaces;
using Restuarant_Management.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarant_Management.Service
{
    public class RestaurantService
    {
        private readonly INamedEntityRepository<Restaurant> _restaurantRepo;

        public RestaurantService(INamedEntityRepository<Restaurant> restaurantRepo)
        {
            _restaurantRepo = restaurantRepo;
        }
        // GET 
        public async Task<IEnumerable<Restaurant>> GetAllModelAsync()
        {
            return await _restaurantRepo.GetAllAsync();
        }

        public async Task<Restaurant?> GetModelByIdAsync(int id)
        {
            return await _restaurantRepo.GetByIdAsync(id);
        }

        public async Task<Restaurant?> GetModelByNameAsync(string name)
        {
            return await _restaurantRepo.GetByNameAsync(name);
        }
        //SEARCH (LINQ filtering)
        public async Task<IEnumerable<Restaurant>> SearchAsync(string? location, double? minRating)
        {
            var allRestaurants = await _restaurantRepo.GetAllAsync();

            // Apply filtering
            var query = allRestaurants.AsQueryable();

            if (!string.IsNullOrEmpty(location))
                query = query.Where(r => r.Location.Contains(location));

            if (minRating.HasValue)
                query = query.Where(r => r.Rating >= minRating.Value);

            return query.ToList();//it actually applies all filters and produces the result.
        }

        // POST 
        public async Task AddAsync(RestaurantDTO dto)
        {
            var entity = new Restaurant
            {
                RestuarantName = dto.RestuarantName,
                Location = dto.Location,
                EstablishedDate = dto.EstablishedDate,
                AverageMealPrice = dto.AverageMealPrice,
                Rating = dto.Rating
            };

            await _restaurantRepo.AddAsync(entity);
            await _restaurantRepo.SaveAsync();
        }
        // PUT 
        public async Task UpdateAsync(int id, RestaurantDTO dto)
        {
            var entity = await _restaurantRepo.GetByIdAsync(id);
            if (entity == null) return;

            // Convert Entity -> DTO
            entity.RestuarantName = dto.RestuarantName;
            entity.Location = dto.Location;
            entity.EstablishedDate = dto.EstablishedDate;
            entity.AverageMealPrice = dto.AverageMealPrice;
            entity.Rating = dto.Rating;

            await _restaurantRepo.UpdateAsync(entity);
            await _restaurantRepo.SaveAsync();
        }
        // DELETE (by Id)
        public async Task DeleteAsync(int id)
        {
            await _restaurantRepo.DeleteAsync(id);
            await _restaurantRepo.SaveAsync();
        }
    }
}
