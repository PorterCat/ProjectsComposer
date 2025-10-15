using ProjectsComposer.Core.Models;

namespace ProjectsComposer.DataAccess.Repository;

public interface IEmployeesRepository
{
    Task<IEnumerable<Employee>> Get();
    Task<Employee?> GetById(Guid id);
    Task Add(Guid id, string userName, string email);
}