using System.Collections.Concurrent;
using ProjectsComposer.Core.Contracts.Conflicts;
using ProjectsComposer.Core.Models.Conflicts;

namespace ProjectsComposer.API.Services.Conflicts;

public class PendingCasesStore : IPendingCasesStore
{
    private readonly ConcurrentDictionary<Guid, PendingProjectCase> _cases = [];
    
    public Task<PendingProjectCase> Create(PendingProjectCase pendingProjectCase)
    {
        _cases[pendingProjectCase.Id] = pendingProjectCase;
        return Task.FromResult(pendingProjectCase);
    }

    public Task<IEnumerable<PendingProjectCase>> GetAllPending() =>
        Task.FromResult(_cases.Values.Where(c => c.Status == PendingProjectCaseStatus.Pending));

    public Task<PendingProjectCase?> GetCase(Guid caseId) =>
        Task.FromResult(_cases.GetValueOrDefault(caseId));

    public Task<bool> Close(Guid caseId) =>
        Task.FromResult(_cases.TryRemove(caseId, out var c));

    public Task<bool> TryGetCaseByTitle(string requestTitle, out PendingProjectCase? pendingProjectCase)
    {
        pendingProjectCase = _cases.Values.FirstOrDefault(c => c.Project.Title == requestTitle);
        return Task.FromResult(pendingProjectCase is not null);
    }
}