using Microsoft.EntityFrameworkCore;
using zintersid.Common.IoC;
using zintersid.Data;
using zintersid.Data.Entities;

namespace zintersid.Repositories
{
    public class UserClaimRepository : RepositoryBase<AppUserClaim>, IUserClaimRepository
    {
        public UserClaimRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AppUserClaim>> GetClaimsByUserIdAsync(string userId)
        {
            return await _context.Set<AppUserClaim>()
                                .Where(claim => claim.UserId == userId)
                                .ToListAsync();
        }
    }

    public interface IUserClaimRepository : IRepository<AppUserClaim>, IScopedRegister
    {
        Task<IEnumerable<AppUserClaim>> GetClaimsByUserIdAsync(string userId);
    }
}