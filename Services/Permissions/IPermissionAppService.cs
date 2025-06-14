using zintersid.Common.IoC;
using zintersid.Data.Entities;

namespace zintersid.Services.Permissions
{
    public interface IPermissionAppService: IAppService, ITransientRegister
    {
        Task<IEnumerable<AppPermission>> GetAllPermissionsAsync();
        Task AddPermissionAsync(AppPermission permission);
        Task UpdatePermissionAsync(int id, AppPermission permission);
        Task DeletePermissionAsync(int id);
    }
}