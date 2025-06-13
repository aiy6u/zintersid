using System.ComponentModel.DataAnnotations;

namespace zintersid.Data.Entities
{
    public class AppPermission
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}