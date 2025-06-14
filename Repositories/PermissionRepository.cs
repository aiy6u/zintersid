using zintersid.Common.IoC;
using zintersid.Data;
using zintersid.Data.Entities;

namespace zintersid.Repositories
{
    public interface IPermissionRepository : IRepository<AppPermission>, IScopedRegister
    {

    }

    public class PermissionRepository : RepositoryBase<AppPermission>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext context) : base(context)
        {
        }
    }
}