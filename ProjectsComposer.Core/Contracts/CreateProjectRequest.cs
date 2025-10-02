using System.ComponentModel.DataAnnotations;
using ProjectsComposer.Core.Models;

namespace ProjectsComposer.Core.Contracts;

public record CreateProjectRequest(
    [Required][MaxLength(Project.MaxTitleLength)] string Title, 
    DateOnly StartDate, 
    DateOnly EndDate,
    Guid LeaderId);