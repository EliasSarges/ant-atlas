using AntAtlas.Domain.Entities;
using AntAtlas.Domain.Services;
using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Tests.Services;

public class SensoryServiceTest
{
    private Colony CreateColony()
    {
        return new Colony(foodStock: new FoodStock(10), foodCost: 1);
    }

    [Fact]
    public void LookAround_WhenHasFood_ShouldReturnFoodLocation()
    {
        var colony = CreateColony();

        Assert.True(colony.TrySpawnAnt(position: new Coordinate(2, 2), energy: 10, out var ant));
        Assert.NotNull(ant);

        var grid = new Grid(5, 5);
        var foodPosition = new Coordinate(3, 3);

        Assert.True(grid.TrySpawnFood(position: foodPosition, amount: 10));

        var sensoryService = new SensoryService();
        var antPerception = sensoryService.LookAround(grid, ant);

        Assert.Single(antPerception.FoodLocations);
        Assert.Contains(foodPosition, antPerception.FoodLocations);
    }

    [Fact]
    public void LookAround_WhenHasMoreThanOneFood_ShouldReturnFoodLocations()
    {
        var colony = CreateColony();

        Assert.True(colony.TrySpawnAnt(position: new Coordinate(2, 2), energy: 10, out var ant));
        Assert.NotNull(ant);

        var grid = new Grid(5, 5);
        var foodPosition = new Coordinate(3, 3);

        Assert.True(grid.TrySpawnFood(position: foodPosition, amount: 10));

        var sensoryService = new SensoryService();
        var antPerception = sensoryService.LookAround(grid, ant);

        Assert.Single(antPerception.FoodLocations);
        Assert.Contains(foodPosition, antPerception.FoodLocations);
    }

    [Fact]
    public void LookAround_WhenIsOnGridOrigin_ShouldReturnJustThreeFreeTiles()
    {
        var colony = CreateColony();

        Assert.True(colony.TrySpawnAnt(position: new Coordinate(0, 0), energy: 10, out var ant));
        Assert.NotNull(ant);

        var grid = new Grid(5, 5);
        var sensoryService = new SensoryService();
        var perception = sensoryService.LookAround(grid, ant);

        Assert.NotEmpty(perception.FreeTiles);
        Assert.Equal(3, perception.FreeTiles.Count);
        Assert.Empty(perception.FoodLocations);
    }
}