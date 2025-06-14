using zintersid.Common.IoC;
using zintersid.Data;
using zintersid.Data.Entities;

namespace zintersid.Repositories
{
    public class UserRoleRepository : RepositoryBase<AppUserRole>, IUserRoleRepository
    {
        public UserRoleRepository(AppDbContext context) : base(context)
        {
        }
    }

    public interface IUserRoleRepository : IRepository<AppUserRole>, IScopedRegister
    {
    }
}