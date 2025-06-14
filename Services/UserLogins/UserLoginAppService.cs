using zintersid.Common.IoC;
using zintersid.Data.Entities;
using zintersid.Repositories;

namespace zintersid.Services.UserLogins
{
    public class UserLoginAppService : AppServiceBase, IUserLoginAppService
    {
        private readonly IUserLoginRepository _userLoginRepository;

        public UserLoginAppService(IUserLoginRepository userLoginRepository)
        {
            _userLoginRepository = userLoginRepository;
        }

        public async Task<AppUserLogin?> GetUserLoginAsync(string userId)
        {
            return await _userLoginRepository.FindSingleAsync(x => x.UserId == userId);
        }

        public async Task AddUserLoginAsync(AppUserLogin userLogin)
        {
            await _userLoginRepository.AddAsync(userLogin);
        }

        public async Task UpdateUserLoginAsync(AppUserLogin userLogin)
        {
            await _userLoginRepository.UpdateAsync(userLogin);
        }

        public async Task DeleteUserLoginAsync(string userId)
        {
            var userLogin = await GetUserLoginAsync(userId);
            if (userLogin != null)
            {
                await _userLoginRepository.DeleteAsync(userLogin);
            }
        }
    }
}