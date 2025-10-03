using Microsoft.AspNetCore.Mvc;
using ProjectsComposer.API.Services;
using ProjectsComposer.Core.Contracts;
using ProjectsComposer.Core.Models;

namespace ProjectsComposer.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController(IEmployeesService employeesService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateEmployee([FromBody] CreateEmployeeRequest request)
    {
        var employeeResult = Employee.Create(Guid.NewGuid(), request.UserName, request.Email);
        if(employeeResult.IsFailure)
            return BadRequest(employeeResult.Error);
        
        var result = await employeesService.CreateEmployee(employeeResult.Value);
        
        if(result.IsFailure)
            return BadRequest(result.Error);
        
        return Created(new Uri($"{Request.Scheme}://{Request.Host}" +
                               $"{Request.Path}/{employeeResult.Value.Id}"), 
            employeeResult.Value.Id);
    }
}