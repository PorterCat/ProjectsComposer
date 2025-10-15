using CSharpFunctionalExtensions;
using ProjectsComposer.Core.Models;

namespace ProjectsComposer.API.Services;

public interface IProjectsService
{
    Task<Project?> GetProject(Guid projectId);
    Task<IEnumerable<Project>> GetProjectsByName(string projectName);
    Task<IEnumerable<Project>> GetProjectsByPage(int pageNum, int pageSize);
    Task<IEnumerable<Project>> GetAllProjects();
    Task<Result> CreateProject(Project project, Guid? leaderId);
    Task<Result<Guid>> DeleteProject(Guid id);
}