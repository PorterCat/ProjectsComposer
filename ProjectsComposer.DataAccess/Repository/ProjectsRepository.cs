using Microsoft.EntityFrameworkCore;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.DataAccess.Repository;

public class ProjectsRepository(ProjectsComposerDbContext dbContext) : IProjectsRepository
{
    public async Task<IEnumerable<ProjectEntity>> Get() => 
        await dbContext.Projects
            .AsNoTracking()
            .OrderBy(c => c.Title)
            .ToListAsync();
    
    public async Task<IEnumerable<ProjectEntity>> GetWithEmployees() =>
        await dbContext.Projects
            .AsNoTracking()
            .Include(c => c.Employees)
            .ToListAsync();
    
    public async Task<ProjectEntity?> GetById(Guid id) =>
        await dbContext.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    
    public async Task<IEnumerable<ProjectEntity>> GetByFilter(string title, int employeesCount)
    {
        var query = dbContext.Projects.AsNoTracking();
        
        if(!string.IsNullOrWhiteSpace(title))
            query = query.Where(c => c.Title.Contains(title));
            
        if(employeesCount > 0)
            query = query.Where(c => c.Employees.Count > employeesCount);
            
        return await query.ToListAsync();
    }
    
    public async Task<IEnumerable<ProjectEntity>> GetByPage(int page, int pageSize) => 
        await dbContext.Projects
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task Add(Guid id, string title, 
        string customerCompanyName, string contractorCompanyName,
        Guid? leaderId,
        DateTime startDate, DateTime? endDate = null)
    {
        var projectEntity = new ProjectEntity
        {
            Id = id,
            Title = title,
            CustomerCompanyName = customerCompanyName,
            ContractorCompanyName = contractorCompanyName,
            StartDate = startDate,
            EndDate = endDate,
            LeaderId = leaderId,
        };
        
        await dbContext.AddAsync(projectEntity);
        await dbContext.SaveChangesAsync();
    }
}