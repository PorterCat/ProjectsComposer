using System.ComponentModel.DataAnnotations;

namespace ProjectsComposer.Core.Contracts.Authorization;

public record LoginRequest(
    [Required] string Username, 
    [Required] string Password);