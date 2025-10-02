using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace ProjectsComposer.Core.Models;

public class Employee
{
    private Employee(Guid id, string userName, string email)
    {
        Id = id;
        UserName = userName;
        Email = email;
    }
    
    public Guid Id { get; }
    public string UserName { get; }
    public string Email { get; }

    public static Result<Employee> Create(Guid id, string userName, string email)
    {
        if(string.IsNullOrEmpty(userName))
            return Result.Failure<Employee>("User name cannot be empty");
        
        if(string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"[a-zA-Z0-9]+\@[a-z]*(\.[a-z]{2,})$"))
            return Result.Failure<Employee>("Invalid email address");
        
        var employee = new Employee(id, userName, email);
        return Result.Success(employee);
    }
}