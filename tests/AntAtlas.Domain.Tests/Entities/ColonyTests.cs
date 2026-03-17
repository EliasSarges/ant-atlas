using AntAtlas.Domain.Entities;
using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Tests.Entities;

public class ColonyTests
{
    private Colony CreateColony(FoodStock? foodStock, int foodCost = 10)
    {
        return new Colony(foodStock ?? new FoodStock(20), foodCost);
    }

    [Fact]
    public void Colony_WhenIsCreatedWithNegativeFoodCost_ShouldThrowException()
    {
        Assert.Throws<InvalidOperationException>(() => CreateColony(foodStock: null, foodCost: -10));
    }

    [Fact]
    public void TrySpawnAnt_WithValidFoodStock_ShouldSpawnAntAndDecreaseFoodStock()
    {
        var colony = CreateColony(foodStock: null, foodCost: 10);
        var antPosition = new Coordinate(0, 0);
        const int energy = 10;

        var result = colony.TrySpawnAnt(antPosition, energy, out var ant);

        Assert.Equal(10, colony.FoodStock.Amount);
        Assert.Equal(1, colony.AntsAliveCount);
        Assert.True(result);
        Assert.NotNull(ant);
    }

    [Fact]
    public void TrySpawnAnt_WithNotEnoughFoodStock_ShouldNotSpawnAntAndNotDecreaseFoodStock()
    {
        var foodStock = new FoodStock(0);
        var colony = CreateColony(foodStock);
        var antPosition = new Coordinate(0, 0);
        const int energy = 10;

        var result = colony.TrySpawnAnt(antPosition, energy, out var ant);

        Assert.Equal(0, colony.FoodStock.Amount);
        Assert.Equal(0, colony.AntsAliveCount);
        Assert.False(result);
        Assert.Null(ant);
    }

    [Fact]
    public void DecreaseAntAlivesCounter_WithZeroAntsAlive_ShouldNotReachNegativeNumber()
    {
        var colony = CreateColony(foodStock: null);

        colony.DecreaseAntAlivesCounter();

        Assert.Equal(0, colony.AntsAliveCount);
    }
}