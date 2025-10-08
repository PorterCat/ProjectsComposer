using Microsoft.EntityFrameworkCore;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.DataAccess.Repository;

public class EmployeesRepository(ProjectsComposerDbContext dbContext) : IEmployeesRepository
{
    public async Task<IEnumerable<EmployeeEntity>> Get() => 
        await dbContext.Employees
            .AsNoTracking()
            .OrderBy(e => e.UserName)
            .ToListAsync();

    public async Task<EmployeeEntity?> GetById(Guid id) =>
        await dbContext.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

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