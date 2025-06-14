using zintersid.Data.Entities;
using Microsoft.AspNetCore.Identity;
using zintersid.Repositories;
using zintersid.Common.IoC;

namespace zintersid.Services.Roles
{
    public class RoleAppService : AppServiceBase, IRoleAppService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RoleAppService(RoleManager<AppRole> roleManager, IRolePermissionRepository rolePermissionRepository)
        {
            _roleManager = roleManager;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<IEnumerable<AppRole>> GetAllRolesAsync()
        {
            return await Task.FromResult(_roleManager.Roles);
        }

        public async Task AddRoleAsync(AppRole role)
        {
            await _roleManager.CreateAsync(role);
        }

        public async Task UpdateRoleAsync(string id, AppRole role)
        {
            var existingRole = await _roleManager.FindByIdAsync(id);
            if (existingRole != null)
            {
                existingRole.Name = role.Name;
                await _roleManager.UpdateAsync(existingRole);
            }
        }

        public async Task DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
        }

        public async Task<bool> ManageRolePermissionsAsync(string roleId, int permissionId, bool isAssign)
        {
            if (isAssign)
            {
                // Logic to assign permission to role
                var rolePermission = new AppRolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                };

                await _rolePermissionRepository.AddAsync(rolePermission);
                return true;
            }
            else
            {
                // Logic to remove permission from role
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
}