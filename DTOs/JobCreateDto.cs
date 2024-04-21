namespace backend_portal_empleos.DTOs
{
    public class JobCreateDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }

}
