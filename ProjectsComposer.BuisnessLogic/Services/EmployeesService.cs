using CSharpFunctionalExtensions;
using ProjectsComposer.Core.Models;
using ProjectsComposer.DataAccess.Entites;
using ProjectsComposer.DataAccess.Repository;

namespace ProjectsComposer.API.Services;

public class EmployeesService(IEmployeesRepository employeesRepository) : IEmployeesService
{
    public Task<IEnumerable<Employee>> GetAllEmployees() =>
        employeesRepository.Get();

    public async Task<Employee?> GetEmployee(Guid id) =>
        await employeesRepository.GetById(id);
    
    public async Task<Result> CreateEmployee(Employee employee)
    {
        await employeesRepository.Add(employee.Id, employee.UserName, employee.Email);
        return Result.Success();
    }
}