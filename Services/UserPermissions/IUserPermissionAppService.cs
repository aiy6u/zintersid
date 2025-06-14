using System.Threading.Tasks;
using zintersid.Common.IoC;
using zintersid.Data.Entities;

namespace zintersid.Services.UserPermissions
{
    public interface IUserPermissionAppService : IAppService, ITransientRegister
    {
        Task<bool> AssignPermissionToUserAsync(string userId, int permissionId);
        Task<bool> RemovePermissionFromUserAsync(string userId, int permissionId);
        Task<IEnumerable<AppPermission>> GetPermissionsByUserIdAsync(string userId);
        Task<IEnumerable<AppPermission>> GetPermissionsByRoleNamesAsync(params string[] roleNames);
        Task<IEnumerable<AppPermission>> GetAllRelevantPermissionsByUserIdAsync(string userId);
    }
}