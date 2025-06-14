using zintersid.Common.IoC;

namespace zintersid.Services.RolePermission
{
    public interface IRolePermissionAppService: IAppService, ITransientRegister
    {
        Task<bool> AssignPermissionToRoleAsync(string roleId, int permissionId);
        Task<bool> RemovePermissionFromRoleAsync(string roleId, int permissionId);
    }
}