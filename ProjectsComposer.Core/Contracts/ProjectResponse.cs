namespace ProjectsComposer.Core.Contracts;

public record ProjectResponse(
    Guid Id,
    string Title,
    string StartDate);