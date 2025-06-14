using zintersid.Common.IoC;
using zintersid.Data.Entities;
using zintersid.Repositories;

namespace zintersid.Services.UserClaims
{
    public class UserClaimAppService : AppServiceBase, IUserClaimAppService
    {
        private readonly IUserClaimRepository _userClaimRepository;

        public UserClaimAppService(IUserClaimRepository userClaimRepository)
        {
            _userClaimRepository = userClaimRepository;
        }

        public async Task<IEnumerable<AppUserClaim>> GetUserClaimsAsync(string userId)
        {
            return await _userClaimRepository.GetClaimsByUserIdAsync(userId);
        }

        public async Task<AppUserClaim?> GetUserClaimByIdAsync(int id)
        {
            return await _userClaimRepository.GetByIdAsync(id);
        }

        public async Task AddUserClaimAsync(AppUserClaim userClaim)
        {
            await _userClaimRepository.AddAsync(userClaim);
        }

        public async Task UpdateUserClaimAsync(AppUserClaim userClaim)
        {
            await _userClaimRepository.UpdateAsync(userClaim);
        }

        public async Task DeleteUserClaimAsync(int id)
        {
            await _userClaimRepository.DeleteAsync(id);
        }
    }
}