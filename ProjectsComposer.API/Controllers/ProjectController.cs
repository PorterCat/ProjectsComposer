using CSharpFunctionalExtensions;
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
public class ProjectController(
    IProjectsService projectsService, 
    IPendingCasesStore pendingCasesStore) : ControllerBase
{
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetAllProjects()
    {
        var projects = await projectsService.GetAllProjects();
        var response = projects.Select(p => new ProjectResponse(p.Id, p.Title, p.StartDate.ToShortDateString()));
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<PageResponse<ProjectResponse>>> GetProjectsByPage([FromQuery] PageQuery pageQuery)
    {
        Result<(IEnumerable<Project> Projects, int Total, int TotalPages)> projects
            = await projectsService.GetProjectsByPage(pageQuery.PageNum, pageQuery.PageSize);

        if (projects.IsFailure)
            return BadRequest(projects.Error);

        var projectResponses = projects.Value.Projects.Select(p =>
            new ProjectResponse(p.Id, p.Title, p.StartDate.ToShortDateString()));

        return Ok(new {projectResponses, projects.Value.Total, projects.Value.TotalPages});
}

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectResponse>> GetProjectById(Guid id)
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
            request.EndDate?.ToDateTime(TimeOnly.MinValue));
        
        if(projectResult.IsFailure)
            return BadRequest(projectResult.Error);

        if (await pendingCasesStore.TryGetCaseByTitle(request.Title, out var pendingProjectCase) 
            && pendingProjectCase is not null)
        {
            return Conflict(pendingProjectCase.Id); // DTO?
        }
        
        var existingProjects = (await projectsService.GetProjectsByName(projectResult.Value.Title)).ToList();
        if (existingProjects.Count != 0)
        {
            List<Guid> cases = [];
            foreach (var existingProject in existingProjects) // Do we really need to check every project?
            {
                var conflictCase = new PendingProjectCase(
                    projectResult.Value, existingProject.Id, 
                    "Title conflict", request.LeaderId);
                
                cases.Add(conflictCase.Id);
                await pendingCasesStore.Create(conflictCase);
            }
            return Conflict(cases); // DTO?
        }
        
        var result = await projectsService.CreateProject(projectResult.Value, request.LeaderId);
        if(result.IsFailure)
            return BadRequest(result.Error);
        
        return Created(
            Url.Action(nameof(GetProjectById), new { id = projectResult.Value.Id }), projectResult.Value);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProject(Guid id)
    {
        var result = await projectsService.DeleteProject(id);
        if(result.IsFailure)
            return BadRequest(result.Error);

        result.TryGetValue(out var idProject);
        return Ok(idProject);
    }
    
    // Conflicts resolutions
    
    [HttpGet("pending-cases/all")]
    public async Task<ActionResult<IEnumerable<PendingProjectCase>>> GetPendingCases()
    {
        var pendingCases = await pendingCasesStore.GetAllPending();
        return Ok(pendingCases); // TODO: DTO!
    }

    [HttpGet("pending-cases/{caseId}")]
    public async Task<ActionResult<PendingProjectCase?>> GetPendingCase(Guid caseId)
    {
        var result = await pendingCasesStore.GetCase(caseId);
        if(result is null)
            return NotFound($"Case [{caseId}] not found.");
        
        return Ok(result);
    }

    [HttpPost("pending-cases/{caseId}/resolve")]
    public async Task<ActionResult<Guid>> ResolveCase(Guid caseId, [FromBody] ResolveRequest request)
    {
        var @case = await pendingCasesStore.GetCase(caseId);
        if(@case is null)
            return NotFound($"Case [{caseId}] not found.");
        
        switch (request.Action)
        {
            case ResolveAction.ReturnExisting:
            {
                await pendingCasesStore.Close(caseId);
                return Ok(await projectsService.GetProject(@case.ConflictProjectId)); // What if we have several of them? 
            }
            case ResolveAction.CreateAnyway:
            {
                var projectResult = await projectsService.CreateProject(@case.Project, @case.LeaderId);
                if (projectResult.IsFailure)
                    return BadRequest(projectResult.Error);

                await pendingCasesStore.Close(caseId);
                return Created(
                    Url.Action(nameof(GetProjectById), new { id = @case.Project.Id }), @case.Project);
            } 
            case ResolveAction.Cancel:
            {
                await pendingCasesStore.Close(caseId);
                return Ok($"Case [{caseId}] is canceled.");   
            }
            default:
                return BadRequest("Not supported action.");
        }
    }
}