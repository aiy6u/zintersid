using Microsoft.AspNetCore.Identity;
using zintersid.Common.Constants;
using zintersid.Data.Entities;

namespace zintersid.Data.Seeders
{
    public static class UserSeeder
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            if (await userManager.FindByNameAsync("admin") == null)
            {
                var user = new AppUser { UserName = "admin", Email = "admin@example.com", FirstName = "Admin", LastName = "Admin" };
                await userManager.CreateAsync(user, AppGlobal.DefaultPassword);
            }

            var adminUser = await userManager.FindByNameAsync("admin");

            if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, AppGlobal.AdminRole))
            {
                await userManager.AddToRoleAsync(adminUser, AppGlobal.AdminRole);
            }
        }
    }
}