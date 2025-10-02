using ProjectsComposer.Core.Models;

namespace ProjectsComposer.DataAccess.Entites;

public class ProjectEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string CustomerCompanyName { get; set; } = string.Empty;
    public string ContractorCompanyName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public Guid? LeaderId { get; set; }
    public EmployeeEntity? Leader { get; set; }
    
    public List<EmployeeEntity> Employees { get; set; } = [];
}