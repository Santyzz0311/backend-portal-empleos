public class JobGetDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string CreatorUserName { get; set; } = string.Empty;
    public string CreatorEmail { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public bool HasApplied { get; set; } // Indica si el usuario ha aplicado a este trabajo
}
