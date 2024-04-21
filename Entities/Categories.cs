using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend_portal_empleos.Entities
{
    public class Categories
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Jobs> Jobs { get; set; } = new List<Jobs>();
    }
}
