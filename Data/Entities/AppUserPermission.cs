using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zintersid.Data.Entities
{
    public class AppUserPermission
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public int PermissionId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser User { get; set; }

        [ForeignKey(nameof(PermissionId))]
        public virtual AppPermission Permission { get; set; }
    }
}