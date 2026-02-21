namespace backend.Model;

public record Event
{
    public required string DateEvent { get; init; }
    public required string VenueId { get; init; }
    public required string Name { get; init; }
}
    