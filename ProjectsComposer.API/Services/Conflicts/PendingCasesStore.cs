using System.Collections.Concurrent;
using ProjectsComposer.Core.Models.Conflicts;

namespace ProjectsComposer.API.Services.Conflicts;

public interface IPendingCasesStore
{
    Task<PendingProjectCase> Create(PendingProjectCase pendingProjectCase);
    Task<IEnumerable<PendingProjectCase>> GetAllPending();
    Task<bool> TryResolve(Guid caseId); // TODO
}

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

    public Task<bool> TryResolve(Guid caseId)
    {
        if(! _cases.TryGetValue(caseId, out var pendingProjectCase)) 
            return Task.FromResult(false);
        return Task.FromResult(true);
    }
}