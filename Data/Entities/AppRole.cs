using Microsoft.AspNetCore.Identity;

namespace zintersid.Data.Entities
{
    public class AppRole : IdentityRole
    {
        // Add additional properties here if needed
        public virtual ICollection<AppRolePermission>? RolePermissions { get; set; }
    }
}