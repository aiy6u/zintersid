using zintersid.Common.IoC;
using zintersid.Data.Entities;
using zintersid.Repositories;

namespace zintersid.Services.UserRoles
{
    public class UserRoleAppService : AppServiceBase, IUserRoleAppService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleAppService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<bool> AssignRoleToUserAsync(string userId, string roleId)
        {
            // Logic to assign role to user
            var userRole = new AppUserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            await _userRoleRepository.AddAsync(userRole);
            return true;
        }

        public async Task<bool> RemoveRoleFromUserAsync(string userId, string roleId)
        {
            // Logic to remove role from user
            var userRole = await _userRoleRepository.FindAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (userRole != null)
            {
                await _userRoleRepository.DeleteAsync(userRole);
                return true;
            }
            return false;
        }
    }
}