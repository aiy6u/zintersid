using System.ComponentModel.DataAnnotations;

namespace zintersid.Data.Entities
{
    public class AppRolePermission
    {
        [Key]
        public int Id { get; set; }

        public string RoleId { get; set; }
        public int PermissionId { get; set; }
        
        public virtual AppRole Role { get; set; }
        public virtual AppPermission Permission { get; set; }
    }
}