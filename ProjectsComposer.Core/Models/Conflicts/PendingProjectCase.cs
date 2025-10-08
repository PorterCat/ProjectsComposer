namespace ProjectsComposer.Core.Models.Conflicts;

public enum PendingProjectCaseStatus {Pending, Resolved, Cancelled, Expired}

public record PendingProjectCase(Project Project, Guid ConflictProjectId, string Context, Guid? LeaderId = null)
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateOnly CreatedAt { get; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public PendingProjectCaseStatus Status { get; set; } = PendingProjectCaseStatus.Pending;
}