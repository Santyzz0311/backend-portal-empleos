using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend_portal_empleos.Entities
{
    public class Jobs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public required DateTime PublicationDate { get; set; } = DateTime.Now;

        [Required]
        public required bool IsOpen { get; set; } = true;

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual Users? User { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Categories? Category { get; set; }

        public virtual ICollection<Applications> Application { get; set; } = new List<Applications>();
    }
}
