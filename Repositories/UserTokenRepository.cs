using Microsoft.EntityFrameworkCore;
using zintersid.Common.IoC;
using zintersid.Data;
using zintersid.Data.Entities;

namespace zintersid.Repositories
{
    public class UserTokenRepository : RepositoryBase<AppUserToken>, IUserTokenRepository
    {
        public UserTokenRepository(AppDbContext context) : base(context)
        {
        }

        public async Task DeleteAllUserToken(string userId)
        {
            var aut = _context.Set<AppUserToken>();
            var targets = await aut.Where(x => x.UserId == userId).ToListAsync();
            if (targets.Count > 0)
            {
                aut.RemoveRange(targets);
            }
        }

        public override Task DeleteAsync<T>(T token)
        {
            return Task.FromResult(0);
        }

        public async Task DeleteRefreshToken(string userId, string refreshToken)
        {
            var aut = _context.Set<AppUserToken>();
            var target = aut.FirstOrDefault(x => x.UserId == userId && x.Value == refreshToken);
            if (target != null)
            {
                aut.Remove(target);
                await _context.SaveChangesAsync();
            }
        }

    }

    public interface IUserTokenRepository : IRepository<AppUserToken>, IScopedRegister
    {
        Task DeleteRefreshToken(string userId, string refreshToken);
        Task DeleteAllUserToken(string userId);
    }
}