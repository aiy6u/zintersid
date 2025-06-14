using System.Threading.Tasks;
using zintersid.Common.IoC;
using zintersid.Data.Entities;

namespace zintersid.Services.UserTokens
{
    public interface IUserTokenAppService : IAppService, ITransientRegister
    {
        Task<IEnumerable<AppUserToken>> GetUserTokensAsync(string userId);
        Task<AppUserToken?> GetUserTokenByIdAsync(string userId);
        Task AddUserTokenAsync(AppUserToken token);
        Task UpdateUserTokenAsync(AppUserToken token);
        Task DeleteUserTokenAsync(string userId);
        Task DeleteRefreshTokenAsync(string userId, string refreshToken);
        Task<AppUserToken?> GetByTokenAsync(string token);
        Task SaveRefreshTokenAsync(string userId, string refreshToken);
    }
}