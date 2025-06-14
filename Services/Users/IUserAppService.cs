using zintersid.Common.IoC;
using zintersid.Data.Entities;

namespace zintersid.Services.Users
{
    public interface IUserAppService: IAppService, ITransientRegister
    {
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser?> GetUserByIdAsync(string id);
        Task AddUserAsync(AppUser user);
        Task UpdateUserAsync(string id, AppUser user);
        Task DeleteUserAsync(string id);
    }
}