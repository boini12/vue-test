using System.Runtime.InteropServices.JavaScript;

namespace backend.Model;

public record StopOver
{
    public required Stop Stop { get; init; }
    public required JSType.Date Arrival { get; init; }
    public required JSType.Date Departure { get; init; }
}