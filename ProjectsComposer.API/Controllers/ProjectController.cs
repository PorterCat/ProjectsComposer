using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectsComposer.API.Services;
using ProjectsComposer.API.Services.Conflicts;
using ProjectsComposer.Core.Contracts;
using ProjectsComposer.Core.Contracts.Conflicts;
using ProjectsComposer.Core.Models;
using ProjectsComposer.Core.Models.Conflicts;

namespace ProjectsComposer.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProjectController(IProjectsService projectsService, IPendingCasesStore pendingCasesStore) : ControllerBase
{
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetAllProjects()
    {
        var projects = await projectsService.GetAllProjects();
        var response = projects.Select(p => new ProjectResponse(p.Id, p.Title, p.StartDate.ToShortDateString()));
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectResponse>> GetProject(Guid id)
    {
        var project = await projectsService.GetProject(id);
        if(project is null)
            return NotFound($"Project [{id}] not found.");
        
        return Ok(new ProjectResponse(project.Id, project.Title, project.StartDate.ToShortDateString()));
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateProject([FromBody] CreateProjectRequest request)
    {
        var projectResult = Project.Create(
            Guid.NewGuid(),
            request.Title,
            request.CustomerCompanyName,
            request.ContractorCompanyName,
            request.StartDate.ToDateTime(TimeOnly.MinValue),
            request.EndDate.ToDateTime(TimeOnly.MinValue));
        
        if(projectResult.IsFailure)
            return BadRequest(projectResult.Error);

        var result = await projectsService.CreateProject(projectResult.Value, request.LeaderId);
        if(result.IsFailure)
            return BadRequest(result.Error);
        
        return Created(new Uri($"{Request.Path}/{projectResult.Value.Id}"), projectResult.Value.Id);
    }
    
    // Conflicts resolutions
    
    [HttpGet("pending-cases")]
    public async Task<ActionResult<IEnumerable<PendingProjectCase>>> GetPendingCases()
    {
        var pendingCases = await pendingCasesStore.GetAllPending();
        return Ok(pendingCases);
    }

    [HttpPost("pending-cases/{caseId}/resolve")]
    public async Task<ActionResult<Guid>> ResolveCase(Guid caseId, [FromBody] ResolveRequest request)
    {
        if (request.Action == ResolveAction.ReturnExisting)
        {
            // pendingCasesStore.
            // return Ok(pendingCasesStore.);
            return Ok();
        }
        else if (request.Action == ResolveAction.CreateAnyway)
        {
            // var result = await projectsService.CreateProject(projectResult.Value, request.LeaderId);
            // if(result.IsFailure)
                //return BadRequest(result.Error);
            return Created(); // TODO: ADD URL COMPLETED 
        }
        else
        {
            return Ok($"Case [{caseId}] is canceled.");
        }
    }
}