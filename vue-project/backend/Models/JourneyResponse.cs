namespace backend.Model;

public record JourneyResponse
{
    public required List<Journey> Journeys { get; init; }
}