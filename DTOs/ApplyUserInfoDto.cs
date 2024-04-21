namespace backend_portal_empleos.DTOs
{
    public class ApplyUserInfoDto
    {
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public DateTime ApplyDate { get; set; }
        public string Description {  get; set; } = string.Empty;

    }
}
