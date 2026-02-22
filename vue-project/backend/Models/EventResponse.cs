namespace backend.Model;

public record EventResponse
{
    public required List<Event> Events { get; init; }
}