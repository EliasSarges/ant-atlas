using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Entities;

public class Grid : Entity
{
    public int Width { get; init; }
    public int Height { get; init; }
    public Dictionary<Coordinate, FoodSource> FoodSources { get; private set; } = new();

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public bool IsWithinBound(Coordinate position)
    {
        var isInWidth = position.X >= 0 && position.X < Width;
        var isInHeight = position.Y >= 0 && position.Y < Height;

        return isInWidth && isInHeight;
    }

    public bool TrySpawnFood(Coordinate position, int amount)
    {
        if (!IsWithinBound(position) || FoodSources.ContainsKey(position))
        {
            return false;
        }

        FoodSources[position] = new FoodSource(position, amount);

        return true;
    }
}