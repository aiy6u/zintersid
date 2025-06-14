using zintersid.Common.IoC;
using zintersid.Data;
using zintersid.Data.Entities;

namespace zintersid.Repositories
{
    public class RolePermissionRepository : RepositoryBase<AppRolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(AppDbContext context) : base(context)
        {
        }
    }

    public interface IRolePermissionRepository: IRepository<AppRolePermission>, IScopedRegister
    {
    }
}