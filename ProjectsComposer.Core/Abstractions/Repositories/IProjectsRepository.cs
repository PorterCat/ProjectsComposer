using ProjectsComposer.Core.Models;

namespace ProjectsComposer.DataAccess.Repository;

public interface IProjectsRepository
{
    Task<IEnumerable<Project>> Get();
    Task<IEnumerable<Project>> GetWithEmployees();
    Task<Project?> GetById(Guid id);
    Task<IEnumerable<Project>> GetByFilter(string title, int employeesCount);
    Task<IEnumerable<Project>> GetByPage(int pageNum, int pageSize);
    Task<int> GetCountAsync();
    Task Add(Guid id, string title,
        string customerCompanyName, string contractorCompanyName,
        Guid? leaderId,
        DateTime startDate, DateTime? endDate = null);

    Task<bool> Delete(Guid id);
}