using Restuarant_Management.Interfaces;
using Restuarant_Management.Models;

namespace Restuarant_Management.Service
{
    public class UserService
    {
        private readonly IRepository<User> _userRepo;

        public UserService(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _userRepo.GetAllAsync();
        public async Task<User?> GetByIdAsync(int id) => await _userRepo.GetByIdAsync(id);

        public async Task AddAsync(User user)
        {
            await _userRepo.AddAsync(user);
            await _userRepo.SaveAsync();
        }

        public async Task UpdateAsync(User user)
        {
            await _userRepo.UpdateAsync(user);
            await _userRepo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepo.DeleteAsync(id);
            await _userRepo.SaveAsync();
        }
    }

}

