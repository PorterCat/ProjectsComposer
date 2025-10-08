using ProjectsComposer.Core.Contracts.Conflicts;
using ProjectsComposer.Core.Models.Conflicts;

namespace ProjectsComposer.API.Services.Conflicts;

public interface IPendingCasesStore
{
    Task<PendingProjectCase> Create(PendingProjectCase pendingProjectCase);
    Task<IEnumerable<PendingProjectCase>> GetAllPending();
    Task<PendingProjectCase?> GetCase(Guid caseId);
    Task<bool> Close(Guid caseId);
    Task<bool> TryGetCaseByTitle(string requestTitle, out PendingProjectCase? pendingProjectCase);
}