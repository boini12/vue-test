namespace backend.Model;

public record JourneyResponse
{
    public required List<JourneyResponse> Journeys { get; init; }
}