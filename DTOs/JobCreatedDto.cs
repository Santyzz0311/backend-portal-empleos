namespace backend_portal_empleos.DTOs
{
    public class JobCreatedDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string CreatorUserName { get; set; } = string.Empty;
        public string CreatorEmail { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
