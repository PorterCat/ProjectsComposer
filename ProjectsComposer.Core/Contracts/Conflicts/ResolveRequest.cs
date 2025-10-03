using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectsComposer.Core.Contracts.Conflicts;

public enum ResolveAction
{
    ReturnExisting,
    CreateAnyway,
    Cancel
}

public class ResolveRequest
{
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter<ResolveAction>))]
    public ResolveAction Action { get; set; }
}