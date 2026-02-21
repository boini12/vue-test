using System.Runtime.InteropServices.JavaScript;

namespace backend.Model;

public record Leg
{
    public required JSType.Date Departure { get; init; }
    public required JSType.Date Arrival { get; init; }
    public required Stop Origin { get; init; }
    public required Stop Destination { get; init; }
    public required IEnumerable<StopOver> StopOvers { get; init; }
    
}