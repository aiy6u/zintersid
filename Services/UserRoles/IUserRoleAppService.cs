using zintersid.Common.IoC;

namespace zintersid.Services.UserRoles
{
    public interface IUserRoleAppService: IAppService, ITransientRegister
    {
        Task<bool> AssignRoleToUserAsync(string userId, string roleId);
        Task<bool> RemoveRoleFromUserAsync(string userId, string roleId);
    }
}