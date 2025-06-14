using zintersid.Common.IoC;
using zintersid.Data.Entities;
using zintersid.Repositories;

namespace zintersid.Services.UserPermissions
{
    public class UserPermissionAppService : AppServiceBase, IUserPermissionAppService
    {
        private readonly IUserPermissionRepository _userPermissionRepository;

        public UserPermissionAppService(IUserPermissionRepository userPermissionRepository)
        {
            _userPermissionRepository = userPermissionRepository;
        }

    public async Task<bool> AssignPermissionToUserAsync(string userId, int permissionId)
        {
            // Logic to assign permission to user
            var userPermission = new AppUserPermission
            {
                UserId = userId,
                PermissionId = permissionId
            };
            await _userPermissionRepository.AddAsync(userPermission);
            return true;
        }

        public async Task<bool> RemovePermissionFromUserAsync(string userId, int permissionId)
        {
            // Logic to remove permission from user
            var userPermission = await _userPermissionRepository.FindAsync(up => up.UserId == userId && up.PermissionId == permissionId);
            if (userPermission != null)
            {
                await _userPermissionRepository.DeleteAsync(userPermission);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<AppPermission>> GetPermissionsByUserIdAsync(string userId)
        {
            return await _userPermissionRepository.GetAppPermissionListByUserIdAsync(userId);
        }

        public Task<IEnumerable<AppPermission>> GetPermissionsByRoleNamesAsync(params string[] roleNames)
        {
            return _userPermissionRepository.GetAppPermissionListByRoleNamesAsync(roleNames);
        }

        public Task<IEnumerable<AppPermission>> GetAllRelevantPermissionsByUserIdAsync(string userId)
        {
            return _userPermissionRepository.GetAllReleventAppPermissionListByUserIdAsync(userId);
        }
    }
}