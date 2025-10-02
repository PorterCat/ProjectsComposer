using Microsoft.AspNetCore.Mvc;
using ProjectsComposer.API.Services;
using ProjectsComposer.Core.Contracts;
using ProjectsComposer.Core.Models;

namespace ProjectsComposer.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController(IProjectsService projectsService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<ActionResult<List<ProjectResponse>>> GetAllProjects()
    {
        var projects = await projectsService.GetAllProjects();
        var response = projects.Select(p => new ProjectResponse(p.Id, p.Title, p.StartDate.ToShortDateString()));
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateProject([FromBody] CreateProjectRequest request)
    {
        var projectResult = Project.Create(
            Guid.NewGuid(),
            request.Title,
            string.Empty,
            string.Empty,
            request.StartDate.ToDateTime(TimeOnly.MinValue),
            request.EndDate.ToDateTime(TimeOnly.MaxValue));
        
        if(projectResult.IsFailure)
            return BadRequest(projectResult.Error);

        var result = await projectsService.CreateProject(projectResult.Value, request.LeaderId);
        if(result.IsFailure)
            return BadRequest(result.Error);
        
        return Created(); // TODO: ADD URL COMPLETED 
    }
}