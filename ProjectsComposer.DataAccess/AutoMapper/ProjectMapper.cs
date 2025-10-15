using CSharpFunctionalExtensions;
using ProjectsComposer.Core.Models;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.Core.AutoMapper;

public static class ProjectMapper 
{
    public static Result<Project> ToDomain(this ProjectEntity entity)
    {
        return Project.Create(
            entity.Id,
            entity.Title,
            entity.ContractorCompanyName,
            entity.CustomerCompanyName,
            entity.StartDate,
            entity.EndDate
        );
    }

    public static IEnumerable<Project> ToDomain(this IEnumerable<ProjectEntity> entities)
    {
        return entities
            .Select(e => e.ToDomain())
            .Where(r => r.IsSuccess)
            .Select(r => r.Value);
    }
}