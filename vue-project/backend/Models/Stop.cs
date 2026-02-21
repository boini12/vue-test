namespace backend.Model;

public record Stop
{
    public required string Id { get; init; }
    public required string Name { get; init; }
}