using zintersid.Common.IoC;
using zintersid.Data.Entities;

namespace zintersid.Services.Roles
{
    public interface IRoleAppService: IAppService, ITransientRegister
    {
        Task<IEnumerable<AppRole>> GetAllRolesAsync();
        Task AddRoleAsync(AppRole role);
        Task UpdateRoleAsync(string id, AppRole role);
        Task DeleteRoleAsync(string id);
        Task<bool> ManageRolePermissionsAsync(string roleId, int permissionId, bool isAssign);
    }
}