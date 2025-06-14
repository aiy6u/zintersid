using zintersid.Common.IoC;
using zintersid.Data.Entities;
using zintersid.Repositories;

namespace zintersid.Services.RolePermission
{
    public class RolePermissionAppService : AppServiceBase, IRolePermissionAppService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionAppService(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<bool> AssignPermissionToRoleAsync(string roleId, int permissionId)
        {
            var rolePermission = new AppRolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            };

            await _rolePermissionRepository.AddAsync(rolePermission);
            return true;
        }

        public async Task<bool> RemovePermissionFromRoleAsync(string roleId, int permissionId)
        {
            var rolePermission = await _rolePermissionRepository.FindAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (rolePermission != null)
            {
                await _rolePermissionRepository.DeleteAsync(rolePermission);
                return true;
            }

            return false;
        }
    }
}