using CSharpFunctionalExtensions;
using ProjectsComposer.Core.Models;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.API.Services;

public interface IProjectsService
{
    Task<ProjectEntity?> GetProject(Guid projectId);
    Task<IEnumerable<ProjectEntity>> GetProjectsByName(string projectName);
    Task<IEnumerable<ProjectEntity>> GetProjectsByPage(int pageNum, int pageSize);
    Task<IEnumerable<ProjectEntity>> GetAllProjects();
    Task<Result> CreateProject(Project project, Guid? leaderId);
    Task<Result<Guid>> DeleteProject(Guid id);
}