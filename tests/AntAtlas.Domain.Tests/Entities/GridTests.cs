using AntAtlas.Domain.Entities;
using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Tests.Entities;

public class GridTests
{
    public static TheoryData<Coordinate> InvalidCoordinates =>
    [
        new Coordinate(-1, -1),
        new Coordinate(-1, 10),
        new Coordinate(10, 10)
    ];

    public static TheoryData<Coordinate> ValidCoordinates =>
    [
        new Coordinate(0, 0),
        new Coordinate(1, 1),
        new Coordinate(9, 9)
    ];

    public Grid CreateGrid()
    {
        return new Grid(10, 10);
    }

    [Theory]
    [MemberData(nameof(InvalidCoordinates))]
    public void IsWithinBound_WithInvalidPosition_ShouldReturnFalse(Coordinate invalidPosition)
    {
        var grid = CreateGrid();

        Assert.False(grid.IsWithinBound(invalidPosition));
    }

    [Theory]
    [MemberData(nameof(ValidCoordinates))]
    public void IsWithinBound_WithValidPosition_ShouldReturnTrue(Coordinate validPosition)
    {
        var grid = CreateGrid();

        Assert.True(grid.IsWithinBound(validPosition));
    }

    [Fact]
    public void TrySpawnFood_WithEqualPositions_ShouldReturnFalse()
    {
        var grid = CreateGrid();
        var position = new Coordinate(1, 1);
        const int amount = 10;

        Assert.True(grid.TrySpawnFood(position, amount));
        Assert.False(grid.TrySpawnFood(position, amount));
    }

    [Fact]
    public void TrySpawnFood_WithDifferentPositions_ShouldReturnTrue()
    {
        var grid = CreateGrid();
        var position1 = new Coordinate(0, 0);
        var position2 = new Coordinate(1, 1);
        const int amount = 10;

        Assert.True(grid.TrySpawnFood(position1, amount));
        Assert.True(grid.TrySpawnFood(position2, amount));
    }
}