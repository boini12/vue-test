namespace backend.Model;

public record EventResponse
{
    public required IEnumerable<Event> Events { get; init; }
}