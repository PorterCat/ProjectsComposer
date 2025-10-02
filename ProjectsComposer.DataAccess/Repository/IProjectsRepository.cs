using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.DataAccess.Repository;

public interface IProjectsRepository
{
    Task<IEnumerable<ProjectEntity>> Get();
    Task<IEnumerable<ProjectEntity>> GetWithEmployees();
    Task<ProjectEntity?> GetById(Guid id);
    Task<IEnumerable<ProjectEntity>> GetByFilter(string title, int employeesCount);
    Task<IEnumerable<ProjectEntity>> GetByPage(int page, int pageSize);

    Task Add(Guid id, string title,
        string customerCompanyName, string contractorCompanyName,
        Guid leaderId,
        DateTime startDate, DateTime? endDate = null);
}