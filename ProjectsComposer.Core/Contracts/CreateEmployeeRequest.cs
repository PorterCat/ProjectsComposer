using System.ComponentModel.DataAnnotations;

namespace ProjectsComposer.Core.Contracts;

public record CreateEmployeeRequest(
    [Required] string UserName, 
    [Required][EmailAddress] string Email);