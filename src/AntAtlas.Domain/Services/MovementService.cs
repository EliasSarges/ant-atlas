using AntAtlas.Domain.Entities;
using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Services;

public class MovementService
{
    public void Move(Ant ant, AntPerception perception)
    {
        var freeTiles = perception.FreeTiles;
        var foodLocations = perception.FoodLocations;

        Coordinate? positionToMove = null;

        if (foodLocations.Count > 0)
        {
            positionToMove = foodLocations[Random.Shared.Next(foodLocations.Count)];
        }

        if (foodLocations.Count == 0 && freeTiles.Count > 0)
        {
            positionToMove = freeTiles[Random.Shared.Next(freeTiles.Count)];
        }

        if (positionToMove == null) return;

        ant.MoveTo(positionToMove, 1);
    }
}