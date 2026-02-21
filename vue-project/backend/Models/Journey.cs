namespace backend.Model;

public record Journey
{
    public required IEnumerable<Leg> Legs { get; init; }
}