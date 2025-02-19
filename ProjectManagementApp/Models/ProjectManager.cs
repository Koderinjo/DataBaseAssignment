namespace ProjectManagementApp.Models
{
    public class ProjectManager
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public List<Project> Projects { get; set; } = [];
    }
}
