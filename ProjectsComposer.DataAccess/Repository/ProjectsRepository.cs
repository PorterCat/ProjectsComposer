using Microsoft.EntityFrameworkCore;
using ProjectsComposer.Core.AutoMapper;
using ProjectsComposer.Core.Models;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.DataAccess.Repository;

public class ProjectsRepository(ProjectsComposerDbContext dbContext) : IProjectsRepository
{
    public async Task<IEnumerable<Project>> Get()
    {
        var result = await dbContext.Projects
            .AsNoTracking()
            .OrderBy(c => c.Title)
            .ToListAsync();

        return result.ToDomain();
    }

    public async Task<IEnumerable<Project>> GetWithEmployees()
    {
        var result = await dbContext.Projects
            .AsNoTracking()
            .Include(c => c.Employees)
            .ToListAsync();   
        
        return result.ToDomain();
    }

    public async Task<Project?> GetById(Guid id)
    {
        var result = await dbContext.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return result?.ToDomain().Value;
    }

    public async Task<IEnumerable<Project>> GetByFilter(string title, int employeesCount)
    {
        var query = dbContext.Projects.AsNoTracking();
        
        if(!string.IsNullOrWhiteSpace(title))
            query = query.Where(c => c.Title.Contains(title));
            
        if(employeesCount > 0)
            query = query
                .Include(p => p.Employees)
                .Where(c => c.Employees.Count > employeesCount);
            
        var result = await query.ToListAsync();
        return result.ToDomain();
    }

    public async Task<IEnumerable<Project>> GetByPage(int pageNum, int pageSize)
    {
        var result = await dbContext.Projects
            .AsNoTracking()
            .Skip(pageNum > 0 ? (pageNum - 1) * pageSize : 0)
            .Take(pageSize)
            .ToListAsync();
        
        return result.ToDomain();
    }

    public async Task<int> GetCountAsync() =>
        await dbContext.Projects.CountAsync();

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

    public async Task<bool> Delete(Guid id) =>
        await dbContext.Projects
            .AsNoTracking()
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync() > 0;
}