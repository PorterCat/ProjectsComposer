namespace ProjectsComposer.DataAccess.Entites;

public class ProjectEntity
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string CustomerCompanyName { get; init; } = string.Empty;
    public string ContractorCompanyName { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    
    public Guid? LeaderId { get; init; }
    public EmployeeEntity? Leader { get; init; }
    
    public List<EmployeeEntity> Employees { get; init; } = [];
}