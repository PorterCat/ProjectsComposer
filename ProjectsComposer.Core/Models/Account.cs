namespace ProjectsComposer.Core.Models;

public class Account
{
    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string PasswordHash { get; set; }
}