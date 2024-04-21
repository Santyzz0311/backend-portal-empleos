using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_portal_empleos.Entities
{
    public class Users
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public required string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public required string Email { get; set; } = string.Empty;

        [Required]
        public required DateTime CreationDate { get; set; } = DateTime.Now;

        [ForeignKey("Rol")]
        [Required]
        public int RolId { get; set; }
        public virtual Roles? Rol { get; set; }

        public virtual ICollection<Applications> Applications { get; set; } = new List<Applications>();
    }
}
