using CSharpFunctionalExtensions;
using ProjectsComposer.Core.Models;
using ProjectsComposer.DataAccess.Entites;
using ProjectsComposer.DataAccess.Repository;

namespace ProjectsComposer.API.Services;

public class ProjectsService(
    IProjectsRepository projectsRepository, 
    IEmployeesRepository employeesRepository) 
    : IProjectsService
{
    public async Task<ProjectEntity?> GetProject(Guid projectId) =>
        await projectsRepository.GetById(projectId);

    public async Task<IEnumerable<ProjectEntity>> GetProjectsByName(string projectName) =>
        await projectsRepository.GetByFilter(projectName, -1);

    public async Task<IEnumerable<ProjectEntity>> GetAllProjects() =>
        await projectsRepository.Get();

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
}