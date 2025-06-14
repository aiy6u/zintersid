using zintersid.Common.IoC;
using zintersid.Data;
using zintersid.Data.Entities;

namespace zintersid.Repositories
{
    public class UserLoginRepository : RepositoryBase<AppUserLogin>, IUserLoginRepository
    {
        public UserLoginRepository(AppDbContext context) : base(context)
        {
        }
    }

    public interface IUserLoginRepository : IRepository<AppUserLogin>, IScopedRegister
    {
    }
}