using System.ComponentModel.DataAnnotations;
using ProjectsComposer.Core.Extensions.ValidationAttributes;
using ProjectsComposer.Core.Models;

namespace ProjectsComposer.Core.Contracts;

public record CreateProjectRequest(
    [Required][MaxLength(Project.MaxTitleLength)] string Title,
    DateOnly StartDate, 
    [DataFuture(ErrorMessage = "EndDate cannot be in the Past")]
    DateOnly? EndDate = null,
    string CustomerCompanyName = "Unknown",
    string ContractorCompanyName = "Unknown",
    Guid? LeaderId = null);