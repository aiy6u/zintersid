using zintersid.Common.IoC;
using zintersid.Data.Entities;
using zintersid.Repositories;

namespace zintersid.Services.Users
{
    public class UserAppService : AppServiceBase, IUserAppService
    {
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<AppUser?> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task AddUserAsync(AppUser user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(string id, AppUser user)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser != null)
            {
                // Update properties
                // existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                // ... other properties

                await _userRepository.UpdateAsync(existingUser);
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}