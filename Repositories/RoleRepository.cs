using zintersid.Common.IoC;
using zintersid.Data;
using zintersid.Data.Entities;

namespace zintersid.Repositories
{
    public class RoleRepository : RepositoryBase<AppRole>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }
    }

    public interface IRoleRepository : IRepository<AppRole>, IScopedRegister
    {
    }
}