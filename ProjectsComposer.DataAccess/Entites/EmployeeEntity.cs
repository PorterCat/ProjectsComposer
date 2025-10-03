namespace ProjectsComposer.DataAccess.Entites;

public class EmployeeEntity
{
    public Guid Id { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    
    public List<ProjectEntity> Projects { get; init; } = [];
    public List<ProjectEntity> LeadingProjects { get; init; } = [];
}