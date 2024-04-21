using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_portal_empleos.Entities
{
    public class Roles
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; } = string.Empty;

        public virtual ICollection<Users> Users { get; set; } = new List<Users>();

    }
}
