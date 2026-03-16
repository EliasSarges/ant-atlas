using AntAtlas.Domain.Entities;
using AntAtlas.Domain.Events;
using AntAtlas.Domain.Exceptions;
using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Tests.Entities;

public class AntTests
{
    private Ant CreateAnt(int energy = 10, Coordinate? position = null)
    {
        var startPosition = position ?? new Coordinate(0, 0);

        return new Ant(startPosition, energy);
    }

    [Fact]
    public void MoveTo_WhenAntIsDead_ShouldThrowAntIsDeadException()
    {
        var ant = CreateAnt(energy: 0);
        var destination = new Coordinate(1, 1);
        const int energyCost = 1;

        Assert.Throws<AntIsDeadException>(() => ant.MoveTo(destination, energyCost));
    }

    [Fact]
    public void MoveTo_WhenAntHasEnergy_ShouldDecreaseEnergyAndChangePosition()
    {
        var ant = CreateAnt(energy: 10);
        var destination = new Coordinate(1, 1);
        const int energyCost = 1;

        ant.MoveTo(destination, energyCost);

        Assert.Equal(9, ant.Energy);
        Assert.Equal(destination, ant.Position);
    }

    [Fact]
    public void MoveTo_WhenAntEnergyReachesZero_ShouldEmitAntDiedEvent()
    {
        var destination = new Coordinate(1, 1);
        var ant = CreateAnt(energy: 1);
        var energyCost = 1;

        ant.MoveTo(destination, energyCost);

        Assert.NotEmpty(ant.DomainEvents);
        Assert.Single(ant.DomainEvents, new AntDied(ant.Id));
    }
}