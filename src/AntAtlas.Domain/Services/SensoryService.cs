using AntAtlas.Domain.Entities;
using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Services;

public class SensoryService
{
    private int FieldOfVision => 1;

    public AntPerception LookAround(Grid grid, Ant ant)
    {
        var currentPosition = ant.Position;
        var currentX = currentPosition.X;
        var currentY = currentPosition.Y;

        var possiblePositions = new Dictionary<Directions, Coordinate>
        {
            [Directions.Left] = currentPosition with { X = currentX - FieldOfVision },
            [Directions.Right] = currentPosition with { X = currentX + FieldOfVision },
            [Directions.Up] = currentPosition with { Y = currentY - FieldOfVision },
            [Directions.Down] = currentPosition with { Y = currentY + FieldOfVision },

            [Directions.TopLeft] = new(X: currentX - FieldOfVision, Y: currentY - FieldOfVision),
            [Directions.TopRight] = new(X: currentX + FieldOfVision, Y: currentY - FieldOfVision),
            [Directions.BottomLeft] = new(X: currentX - FieldOfVision, Y: currentY + FieldOfVision),
            [Directions.BottomRight] = new(X: currentX + FieldOfVision, Y: currentY + FieldOfVision)
        };

        var positionsInBound = possiblePositions.Values.Where(grid.IsWithinBound);
        var freeTiles = new List<Coordinate>();
        var foodLocations = new List<Coordinate>();

        foreach (var possiblePosition in positionsInBound)
        {
            if (grid.FoodSources.ContainsKey(possiblePosition))
            {
                foodLocations.Add(possiblePosition);
                continue;
            }

            freeTiles.Add(possiblePosition);
        }

        return new AntPerception(foodLocations, freeTiles);
    }
}

public record AntPerception(List<Coordinate> FoodLocations, List<Coordinate> FreeTiles);

enum Directions
{
    Left,
    Right,
    Up,
    Down,
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}