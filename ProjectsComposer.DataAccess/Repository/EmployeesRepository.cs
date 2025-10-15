using Microsoft.EntityFrameworkCore;
using ProjectsComposer.Core.AutoMapper;
using ProjectsComposer.Core.Models;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.DataAccess.Repository;

public class EmployeesRepository(ProjectsComposerDbContext dbContext) : IEmployeesRepository
{
    public async Task<IEnumerable<Employee>> Get()
    {
        var result =  await dbContext.Employees
            .AsNoTracking()
            .OrderBy(e => e.UserName)
            .ToListAsync();
        
        return result.ToDomain();
    }

    public async Task<Employee?> GetById(Guid id)
    {
        var result = await dbContext.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);   
        
        return result?.ToDomain().Value;
    }

    public async Task Add(Guid id, string userName, string email)
    {
        var employeeEntity = new EmployeeEntity()
        {
            Id = id,
            UserName = userName,
            Email = email
        };
        
        await dbContext.Employees.AddAsync(employeeEntity);
        await dbContext.SaveChangesAsync();
    }
}