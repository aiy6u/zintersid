using Microsoft.AspNetCore.Identity;
using zintersid.Data.Entities;

namespace zintersid.Data.Seeders
{
    public static class SeederExtensions
    {
        public static async Task SeedDatabase(this IApplicationBuilder app)
        {
            // Ensure the database is created
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                // Ensure the database is created
                var permissionSeeder = new AppPermissionSeeder(dbContext);
                await permissionSeeder.SeedPermissions();

                await RoleSeeder.SeedRoles(roleManager);

                var rolePermissionSeeder = new AppRolePermissionSeeder(dbContext);
                await rolePermissionSeeder.Seed();

                await UserSeeder.SeedUsers(userManager);
            }
        }
    }
}