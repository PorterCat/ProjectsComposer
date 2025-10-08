using Microsoft.AspNetCore.Mvc;
using ProjectsComposer.API.Services;
using ProjectsComposer.Core.Contracts;
using ProjectsComposer.Core.Models;

namespace ProjectsComposer.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController(IEmployeesService employeesService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetAllEmployees()
    {
        var employees = await employeesService.GetAllEmployees();
        return Ok(employees);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(Guid id)
    {
        var result = await employeesService.GetEmployee(id);
        if(result is null)
            return NotFound($"Employee [{id}] not found.");
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateEmployee([FromBody] CreateEmployeeRequest request)
    {
        var employeeResult = Employee.Create(Guid.NewGuid(), request.UserName, request.Email);
        if(employeeResult.IsFailure)
            return BadRequest(employeeResult.Error);
        
        var result = await employeesService.CreateEmployee(employeeResult.Value);
        
        if(result.IsFailure)
            return BadRequest(result.Error);
        
        return Created(
            Url.Action(nameof(GetEmployee), new { id = employeeResult.Value.Id }), employeeResult.Value);
    }
}