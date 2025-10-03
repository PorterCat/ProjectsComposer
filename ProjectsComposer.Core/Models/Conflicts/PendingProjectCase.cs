namespace ProjectsComposer.Core.Models.Conflicts;

public enum PendingProjectCaseStatus {Pending, Resolved, Cancelled, Expired}

public record PendingProjectCase(string Context, IEnumerable<Guid> ConflictProjectIds)
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CreatedAtUtc { get; } = DateTime.UtcNow;
    public PendingProjectCaseStatus Status { get; set; } = PendingProjectCaseStatus.Pending;

    public string Context { get; } = Context;
    public IEnumerable<Guid> ConflictProjectIds { get; } = ConflictProjectIds;
}