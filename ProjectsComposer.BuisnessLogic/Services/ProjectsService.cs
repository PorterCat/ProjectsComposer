using CSharpFunctionalExtensions;
using ProjectsComposer.Core.Models;
using ProjectsComposer.DataAccess.Repository;

namespace ProjectsComposer.API.Services;

public class ProjectsService(
    IProjectsRepository projectsRepository, 
    IEmployeesRepository employeesRepository) 
    : IProjectsService
{
    public async Task<Project?> GetProject(Guid projectId)
    {
        var projectEntity = await projectsRepository.GetById(projectId);
        if (projectEntity is null)
            return null;
        
        var result = Project.Create(projectEntity.Id, projectEntity.Title, 
                projectEntity.ContractorCompanyName, projectEntity.CustomerCompanyName, 
                projectEntity.StartDate, projectEntity.EndDate);

        return result.Value;
    }

    public async Task<IEnumerable<Project>> GetProjectsByName(string projectName)
    {
        var projectEntities = await projectsRepository.GetByFilter(projectName, -1);
        var results = projectEntities.Select(c => 
            Project.Create(c.Id, c.Title, 
                c.ContractorCompanyName, c.CustomerCompanyName, 
                c.StartDate, c.EndDate));
            
        return results
            .Where(r => r.IsSuccess)
            .Select(r => r.Value)
            .ToList();
    }

    public async Task<IEnumerable<Project>> GetProjectsByPage(int pageNum, int pageSize)
    {
        var projectEntities = await projectsRepository.GetByPage(pageNum, pageSize);
        var results = projectEntities.Select(c => 
            Project.Create(c.Id, c.Title, 
            c.ContractorCompanyName, c.CustomerCompanyName, 
            c.StartDate, c.EndDate));
            
        return results
            .Where(r => r.IsSuccess)
            .Select(r => r.Value)
            .ToList();
    }

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
       var projectEntities = await projectsRepository.Get();
       var results = projectEntities.Select(c => 
           Project.Create(c.Id, c.Title, 
               c.ContractorCompanyName, c.CustomerCompanyName, 
               c.StartDate, c.EndDate));
            
       return results
           .Where(r => r.IsSuccess)
           .Select(r => r.Value)
           .ToList();
    }

    public async Task<Result> CreateProject(Project project, Guid? leaderId)
    {
        if(leaderId is not null && await employeesRepository.GetById(leaderId.Value) is not null)
            return Result.Failure($"Leader with {leaderId} doesn't exist");
        
        await projectsRepository.Add(project.Id, project.Title,
            project.CustomerCompanyName, project.ContractorCompanyName,
            leaderId,
            project.StartDate, project.EndDate);
        
        return Result.Success();
    }

    public async Task<Result<Guid>> DeleteProject(Guid id)
    {
        var result = await projectsRepository.Delete(id);
        return result is false ? 
            Result.Failure<Guid>($"Project with {id} doesn't exist") : 
            Result.Success(id);
    }
}