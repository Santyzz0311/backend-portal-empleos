namespace backend_portal_empleos.DTOs
{
    public class ApplyGetDto
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public DateTime AppliedOn { get; set; }
        public string ApplyDescription { get; set; } = string.Empty;
        public string JobLocation { get; set; } = string.Empty;
        public string JobCreatorName { get; set; } = string.Empty;
        public string JobCreatorEmail { get; set; } = string.Empty;
    }
}
