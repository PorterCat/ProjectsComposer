namespace ProjectsComposer.Core.Contracts;

public record PageResponse<T>(
    IEnumerable<T> Results,
    int Total,
    int TotalPages);