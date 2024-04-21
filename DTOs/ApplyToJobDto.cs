namespace backend_portal_empleos.DTOs
{
    public class ApplyToJobDto
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
