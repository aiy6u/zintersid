using zintersid.Common.IoC;
using zintersid.Data.Entities;

namespace zintersid.Services.UserLogins
{
    public interface IUserLoginAppService: IAppService, ITransientRegister
    {
        Task<AppUserLogin?> GetUserLoginAsync(string userId);
        Task AddUserLoginAsync(AppUserLogin userLogin);
        Task UpdateUserLoginAsync(AppUserLogin userLogin);
        Task DeleteUserLoginAsync(string userId);
    }
}