using CSharpFunctionalExtensions;
using ProjectsComposer.Core.Models;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.API.Services;

public interface IEmployeesService
{
    Task<IEnumerable<EmployeeEntity>> GetAllEmployees();
    Task<EmployeeEntity?> GetEmployee(Guid id);
    Task<Result> CreateEmployee(Employee employee);
}