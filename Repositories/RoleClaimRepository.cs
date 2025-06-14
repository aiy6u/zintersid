using zintersid.Common.IoC;
using zintersid.Data;
using zintersid.Data.Entities;


namespace zintersid.Repositories
{
    public class RoleClaimRepository : RepositoryBase<AppRoleClaim>, IRoleClaimRepository
    {
        public RoleClaimRepository(AppDbContext context) : base(context)
        {
        }
    }

    public interface IRoleClaimRepository : IRepository<AppRoleClaim>, IScopedRegister
    {
    }
}