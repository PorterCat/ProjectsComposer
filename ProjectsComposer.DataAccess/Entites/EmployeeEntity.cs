namespace ProjectsComposer.DataAccess.Entites;

public class EmployeeEntity
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public List<ProjectEntity> Projects { get; set; } = [];
    public List<ProjectEntity> LeadingProjects { get; set; } = [];
}