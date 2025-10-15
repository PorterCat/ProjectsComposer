using CSharpFunctionalExtensions;
using ProjectsComposer.Core.Models;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.Core.AutoMapper;

public static class EntityMapper
{
    public static Result<Employee> ToDomain(this EmployeeEntity entity)
    {
        return Employee.Create(
            entity.Id,
            entity.UserName,
            entity.Email
        );
    }

    public static IEnumerable<Employee> ToDomain(this IEnumerable<EmployeeEntity> entities)
    {
        return entities
            .Select(e => e.ToDomain())
            .Where(r => r.IsSuccess)
            .Select(r => r.Value);
    }
}