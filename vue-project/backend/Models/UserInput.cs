using System.Runtime.InteropServices.JavaScript;

namespace backend.Model;

public record UserInput
{
    public required JSType.Date TravelDate { get; init; }
    public required int OriginIndex { get; init; }
    public required int DestinationIndex { get; init; }
    public required JourneyTimeSelection JourneyTimeSelection { get; init; }
}