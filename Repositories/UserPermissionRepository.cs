using Microsoft.EntityFrameworkCore;
using zintersid.Common.IoC;
using zintersid.Data;
using zintersid.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace zintersid.Repositories
{
    /// <summary>
    /// Repository for managing user permissions.
    /// </summary>
    public class UserPermissionRepository : RepositoryBase<AppUserPermission>, IUserPermissionRepository
    {

        public UserPermissionRepository([NotNull] AppDbContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AppPermission>> GetAppPermissionListByUserIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));

            var appUserPermissionQuery = _context.Set<AppUserPermission>().AsQueryable();
            var appPermissionQuery = _context.Set<AppPermission>().AsQueryable();

            var query = appUserPermissionQuery
                .Where(up => up.UserId == userId)
                .Join(appPermissionQuery,
                    up => up.PermissionId,
                    p => p.Id,
                    (up, p) => p);

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AppPermission>> GetAppPermissionListByRoleNamesAsync(params string[] roleNames)
        {
            if (roleNames == null || roleNames.Length == 0)
                return Enumerable.Empty<AppPermission>();

            var roleNameSet = roleNames.ToHashSet();

            var appRoleQuery = _context.Set<AppRole>().AsQueryable();
            var appRolePermissionQuery = _context.Set<AppRolePermission>().AsQueryable();
            var appPermissionQuery = _context.Set<AppPermission>().AsQueryable();

            var query = appRoleQuery
                .Where(r => roleNameSet.Contains(r.Name!))
                .Join(appRolePermissionQuery,
                      r => r.Id,
                      rp => rp.RoleId,
                      (r, rp) => rp)
                .Join(appPermissionQuery,
                      rp => rp.PermissionId,
                      p => p.Id,
                      (rp, p) => p);

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AppPermission>> GetAllReleventAppPermissionListByUserIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));

            var appUserPermissionQuery = _context.Set<AppUserPermission>().AsQueryable();
            var appPermissionQuery = _context.Set<AppPermission>().AsQueryable();
            var appUserRoleQuery = _context.Set<AppUserRole>().AsQueryable();
            var appRoleQuery = _context.Set<AppRole>().AsQueryable();
            var appRolePermissionQuery = _context.Set<AppRolePermission>().AsQueryable();

            // Permissions directly assigned to user
            var userPermissions = appUserPermissionQuery
                .Where(up => up.UserId == userId)
                .Join(appPermissionQuery,
                    up => up.PermissionId,
                    p => p.Id,
                    (up, p) => p);

            // Permissions assigned via user's roles
            var userRoleIds = appUserRoleQuery
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId);

            var rolePermissions = appRoleQuery
                .Where(r => userRoleIds.Contains(r.Id))
                .Join(appRolePermissionQuery,
                    r => r.Id,
                    rp => rp.RoleId,
                    (r, rp) => rp)
                .Join(appPermissionQuery,
                    rp => rp.PermissionId,
                    p => p.Id,
                    (rp, p) => p);

            // Combine and remove duplicates
            var allPermissions = userPermissions
                .Union(rolePermissions)
                .Distinct();

            return await allPermissions.ToListAsync().ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Interface for user permission repository.
    /// </summary>
    public interface IUserPermissionRepository : IRepository<AppUserPermission>, IScopedRegister
    {
        /// <summary>
        /// Gets permissions assigned directly to a user.
        /// </summary>
        Task<IEnumerable<AppPermission>> GetAppPermissionListByUserIdAsync(string userId);

        /// <summary>
        /// Gets permissions assigned to roles by their names.
        /// </summary>
        Task<IEnumerable<AppPermission>> GetAppPermissionListByRoleNamesAsync(params string[] roleNames);

        /// <summary>
        /// Gets all permissions relevant to a user (direct and via roles).
        /// </summary>
        Task<IEnumerable<AppPermission>> GetAllReleventAppPermissionListByUserIdAsync(string userId);
    }
}