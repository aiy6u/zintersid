using zintersid.Common.IoC;
using zintersid.Data.Entities;
using zintersid.Repositories;

namespace zintersid.Services.Permissions
{
    public class PermissionAppService : AppServiceBase, IPermissionAppService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionAppService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<AppPermission>> GetAllPermissionsAsync()
        {
            return await _permissionRepository.GetAllAsync();
        }

        public async Task AddPermissionAsync(AppPermission permission)
        {
            await _permissionRepository.AddAsync(permission);
        }

        public async Task UpdatePermissionAsync(int id, AppPermission permission)
        {
            var existingPermission = await _permissionRepository.GetByIdAsync(id);
            if (existingPermission != null)
            {
                existingPermission.Name = permission.Name;
                existingPermission.Description = permission.Description;
                await _permissionRepository.UpdateAsync(existingPermission);
            }
        }

        public async Task DeletePermissionAsync(int id)
        {
            var permission = await _permissionRepository.GetByIdAsync(id);
            if (permission != null)
            {
                await _permissionRepository.DeleteAsync(permission);
            }
        }
    }
}