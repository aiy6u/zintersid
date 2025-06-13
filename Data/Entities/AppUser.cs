using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace zintersid.Data.Entities
{
    public class AppUser : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<AppUserPermission> UserPermissions { get; set; }
    }
}