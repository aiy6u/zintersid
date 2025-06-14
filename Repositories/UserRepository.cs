using zintersid.Common.IoC;
using zintersid.Data;
using zintersid.Data.Entities;

namespace zintersid.Repositories
{
    public class UserRepository : RepositoryBase<AppUser>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }

    public interface IUserRepository : IRepository<AppUser>, IScopedRegister
    {
    }
}