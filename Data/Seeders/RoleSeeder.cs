using Microsoft.AspNetCore.Identity;
using zintersid.Common.Constants;
using zintersid.Data.Entities;

namespace zintersid.Data.Seeders
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(RoleManager<AppRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(AppGlobal.AdminRole))
            {
                var role = new AppRole { Name = AppGlobal.AdminRole };
                await roleManager.CreateAsync(role);
            }
        }
    }
}