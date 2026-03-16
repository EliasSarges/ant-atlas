using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Tests.ValueObjects;

public class PheromoneTests
{
    private Pheromone CreatePheromone(int strength)
    {
        return new Pheromone(strength, PheromoneType.Food);
    }

    [Fact]
    public void PheromoneStrength_WhenIsCreatedWithNegativeValue_ShouldThrowException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreatePheromone(-500));
    }

    [Fact]
    public void Evaporate_WhenPheromoneHasStrength_ShouldDecreaseCorrectly()
    {
        var pheromone = CreatePheromone(10);

        var newPheromone = pheromone.Evaporate(1);

        Assert.Equal(9, newPheromone.Strength);
    }

    [Fact]
    public void Evaporate_WhenPheromoneHasStrength_NeverDecreaseStrengthLessThanZero()
    {
        var pheromone = CreatePheromone(1);

        var newPheromone = pheromone.Evaporate(2);

        Assert.Equal(0, newPheromone.Strength);
    }
}