using System.ComponentModel.DataAnnotations;

namespace ProjectsComposer.Core.Extensions.ValidationAttributes;

public class DataFutureAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if(value is DateOnly date)
            return date >= DateOnly.FromDateTime(DateTime.Now);
        return false;
    }
}