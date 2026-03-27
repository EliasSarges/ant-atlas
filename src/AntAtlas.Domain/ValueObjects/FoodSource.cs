namespace AntAtlas.Domain.ValueObjects;

public record FoodSource
{
    public Coordinate Position { get; init; }
    public FoodStock Stock { get; init; }

    public FoodSource(Coordinate position, int amount)
    {
        Position = position;
        Stock = new FoodStock(amount);
    }
}