using CSharpFunctionalExtensions;

namespace ProjectsComposer.Core.Models;

public record Project
{
    public const int MaxTitleLength = 250;

    private Project(Guid id, string title, 
        string customerCompanyName, string contractorCompanyName, 
        DateTime startDate, DateTime? endDate = null)
    {
        Id = id;
        Title = title;
        CustomerCompanyName = customerCompanyName;
        ContractorCompanyName = contractorCompanyName;
        StartDate = startDate;
        EndDate = endDate;
    }
    
    public Guid Id { get; }
    public string Title { get; }
    public string CustomerCompanyName { get; }
    public string ContractorCompanyName { get; }
    public DateTime StartDate { get; }
    public DateTime? EndDate { get; }
    
    public static Result<Project> Create(Guid id, string title, 
        string customerCompanyName, string contractorCompanyName, 
        DateTime startDate, DateTime? endDate = null)
    {   
        if(string.IsNullOrEmpty(title))
            return Result.Failure<Project>("Title cannot be empty");
        
        if(startDate < DateTime.UtcNow.Date)
            return Result.Failure<Project>("Start date cannot be in the past");
        
        var employee = new Project(id, title, customerCompanyName, contractorCompanyName, startDate, endDate);
        return Result.Success(employee);
    }
}