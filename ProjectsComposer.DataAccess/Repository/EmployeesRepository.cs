using Microsoft.EntityFrameworkCore;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.DataAccess.Repository;

public interface IEmployeesRepository
{
    Task<IEnumerable<EmployeeEntity>> Get();
    Task<bool> Exist(Guid id);
}

public class EmployeesRepository(ProjectsComposerDbContext dbContext) : IEmployeesRepository
{
    public async Task<IEnumerable<EmployeeEntity>> Get() => 
        await dbContext.Employees
            .AsNoTracking()
            .OrderBy(e => e.UserName)
            .ToListAsync();

    public async Task<bool> Exist(Guid id) =>
        await dbContext.Employees.AnyAsync(e => e.Id == id);
}