namespace ProjectsComposer.Core.Contracts;

public record PageQuery(
    int PageNum,
    int PageSize = 20);