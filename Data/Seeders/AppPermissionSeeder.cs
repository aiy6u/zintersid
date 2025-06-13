using zintersid.Common.Constants;
using zintersid.Common.Helpers;
using zintersid.Data.Entities;

namespace zintersid.Data.Seeders
{
    public class AppPermissionSeeder
    {
        private readonly AppDbContext _context;

        public AppPermissionSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedPermissions()
        {
            // Use ReflectionHelper to get all permissions with descriptions
            var permissionsWithDescriptions = ReflectionHelper.GetConstantsWithDescriptions(typeof(AppPermissions));

            foreach (var kvp in permissionsWithDescriptions)
            {
                // Check if the permission already exists in the database
                if (!_context.AppPermissions.Any(p => p.Name == kvp.Key))
                {
                    // Add new permission to the database
                    _context.AppPermissions.Add(new AppPermission
                    {
                        Name = kvp.Key,
                        Description = kvp.Value
                    });
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
    }
}