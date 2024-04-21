using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_portal_empleos.Entities
{
    public class Applications
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime ApplyDate { get; set; } = DateTime.Now;

        // Relación con la tabla Users
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }
        public virtual Users? User { get; set; }

        // Descripcion motivo para aplicar
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsAccepted { get; set; }

        // Relación con la tabla Jobs
        [ForeignKey("Job")]
        [Required]
        public int JobId { get; set; }
        public virtual Jobs? Job { get; set; }
    }
}
