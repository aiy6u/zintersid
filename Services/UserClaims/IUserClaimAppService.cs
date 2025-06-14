using zintersid.Common.IoC;
using zintersid.Data.Entities;

namespace zintersid.Services.UserClaims
{
    public interface IUserClaimAppService: IAppService, ITransientRegister
    {
        Task<IEnumerable<AppUserClaim>> GetUserClaimsAsync(string userId);
        Task<AppUserClaim?> GetUserClaimByIdAsync(int id);
        Task AddUserClaimAsync(AppUserClaim userClaim);
        Task UpdateUserClaimAsync(AppUserClaim userClaim);
        Task DeleteUserClaimAsync(int id);
    }
}