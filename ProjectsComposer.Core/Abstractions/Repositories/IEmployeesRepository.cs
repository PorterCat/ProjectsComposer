using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.DataAccess.Repository;

public interface IEmployeesRepository
{
    Task<IEnumerable<EmployeeEntity>> Get();
    Task<EmployeeEntity?> GetById(Guid id);
    Task Add(Guid id, string userName, string email);
}