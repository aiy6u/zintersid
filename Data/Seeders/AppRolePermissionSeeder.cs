using Microsoft.EntityFrameworkCore;
using zintersid.Common.Constants;
using zintersid.Common.Helpers;
using zintersid.Data.Entities;

namespace zintersid.Data.Seeders
{
    public class AppRolePermissionSeeder
    {
        private readonly AppDbContext _context;

        public AppRolePermissionSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            var adminRole = await _context.Set<AppRole>().FirstOrDefaultAsync(r => r.Name == AppGlobal.AdminRole);
            if (adminRole != null)
            {
                var permissions = await _context.Set<AppPermission>().ToListAsync();
                foreach (var permission in permissions)
                {
                    // Check if the role already has this permission
                    if (!await _context.Set<AppRolePermission>()
                        .AnyAsync(rp => rp.RoleId == adminRole.Id && rp.PermissionId == permission.Id))
                    {
                        // Add the permission to the role
                        _context.Set<AppRolePermission>().Add(new AppRolePermission
                        {
                            RoleId = adminRole.Id,
                            PermissionId = permission.Id
                        });
                    }
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

            }
        }
    }
}