using CSharpFunctionalExtensions;
using ProjectsComposer.Core.Models;

namespace ProjectsComposer.API.Services;

public interface IEmployeesService
{
    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<Employee?> GetEmployee(Guid id);
    Task<Result> CreateEmployee(Employee employee);
}