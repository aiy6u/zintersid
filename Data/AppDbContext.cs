using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using zintersid.Data.Entities;

namespace zintersid.Data;

// dotnet ef migrations add Init
// dotnet ef database update
public class AppDbContext : IdentityDbContext<
        AppUser,
        AppRole,
        string,
        AppUserClaim,
        AppUserRole,
        AppUserLogin,
        AppRoleClaim,
        AppUserToken>
{

    public DbSet<AppPermission> AppPermissions { get; set; }
    public DbSet<AppRolePermission> AppRolePermissions { get; set; }
    public DbSet<AppUserPermission> AppUserPermissions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Change table names for Identity entities
        builder.Entity<AppUser>().ToTable("AppUser");
        builder.Entity<AppRole>().ToTable("AppRole");
        builder.Entity<AppUserClaim>().ToTable("AppUserClaim");
        builder.Entity<AppUserRole>().ToTable("AppUserRole");
        builder.Entity<AppUserLogin>().ToTable("AppUserLogin");
        builder.Entity<AppRoleClaim>().ToTable("AppRoleClaim");
        builder.Entity<AppUserToken>().ToTable("AppUserToken");

        // Additional custom configurations can be added here
    }
}