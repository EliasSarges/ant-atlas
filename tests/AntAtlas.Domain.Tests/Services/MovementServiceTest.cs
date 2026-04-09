using AntAtlas.Domain.Entities;
using AntAtlas.Domain.Services;
using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Tests.Services;

public class MovementServiceTest
{
    public static TheoryData<List<Coordinate>> FreeTiles =>
    [
        [new Coordinate(1, 1)],
        [new Coordinate(1, 1), new Coordinate(0, 1), new Coordinate(1, 0)]
    ];

    public static TheoryData<List<Coordinate>> FoodLocations =>
    [
        [new Coordinate(1, 1)],
        [new Coordinate(1, 1), new Coordinate(0, 1), new Coordinate(1, 0)]
    ];

    [Theory]
    [MemberData(nameof(FoodLocations))]
    public void Move_WhenPerceptionHasFoodLocations_ShouldMoveAntToFoodLocation(List<Coordinate> foodLocations)
    {
        var freeTiles = new List<Coordinate>();
        var perception = new AntPerception(foodLocations, freeTiles);
        var ant = new Ant(position: new Coordinate(0, 0), 2);

        var movementService = new MovementService();
        movementService.Move(ant, perception);

        Assert.NotEqual(new Coordinate(0, 0), ant.Position);
        Assert.Contains(ant.Position, perception.FoodLocations);
    }

    [Theory]
    [MemberData(nameof(FreeTiles))]
    public void Move_WhenPerceptionHasNoFoodLocationsAndHasFreeTiles_ShouldMoveToFreeTile(List<Coordinate> freeTiles)
    {
        var foodLocations = new List<Coordinate>();
        var perception = new AntPerception(foodLocations, freeTiles);
        var ant = new Ant(position: new Coordinate(0, 0), 2);

        var movementService = new MovementService();
        movementService.Move(ant, perception);

        Assert.NotEqual(new Coordinate(0, 0), ant.Position);
        Assert.Contains(ant.Position, perception.FreeTiles);
    }

    [Fact]
    public void Move_WhenHasNoFreeTilesAndFoodLocations_ShouldNotMoveAnt()
    {
        var foodLocations = new List<Coordinate>();
        var freeTiles = new List<Coordinate>();
        var perception = new AntPerception(foodLocations, freeTiles);
        var ant = new Ant(position: new Coordinate(0, 0), 2);

        var movementService = new MovementService();
        movementService.Move(ant, perception);

        Assert.Equal(new Coordinate(0, 0), ant.Position);
    }
}