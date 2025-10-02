using CSharpFunctionalExtensions;
using ProjectsComposer.Core.Models;
using ProjectsComposer.DataAccess.Entites;
using ProjectsComposer.DataAccess.Repository;

namespace ProjectsComposer.API.Services;

public interface IProjectsService
{
    Task<IEnumerable<ProjectEntity>> GetAllProjects();
    Task<Result> CreateProject(Project project, Guid leaderId);
}

public class ProjectsService(IProjectsRepository projectRepository, 
    IEmployeesRepository employeesRepository) 
    : IProjectsService
{
    public async Task<IEnumerable<ProjectEntity>> GetAllProjects() =>
        await projectRepository.Get();

    public async Task<Result> CreateProject(Project project, Guid leaderId)
    {
        if(!await employeesRepository.Exist(leaderId))
            return Result.Failure($"Leader with {leaderId} doesn't exist");
        
        await projectRepository.Add(project.Id, project.Title,
            project.CustomerCompanyName, project.ContractorCompanyName,
            leaderId,
            project.StartDate, project.EndDate);
        
        return Result.Success();
    }
}