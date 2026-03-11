using AntAtlas.Domain.Entities;
using AntAtlas.Domain.Events;
using AntAtlas.Domain.Exceptions;
using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Tests.Entities;

public class AntTests
{
    [Fact]
    public void MoveTo_WhenAntIsDead_ShouldThrowAntIsDeadException()
    {
        var startPosition = new Coordinate(0, 0);
        var ant = new Ant(startPosition, 0);

        var destination = new Coordinate(1, 1);
        var energyCost = 1;

        Assert.Throws<AntIsDeadException>(() => ant.MoveTo(destination, energyCost));
    }

    [Fact]
    public void MoveTo_WhenAntHasEnergy_ShouldDecreaseEnergyAndChangePosition()
    {
        var startPosition = new Coordinate(0, 0);
        var destination = new Coordinate(1, 1);
        var ant = new Ant(startPosition, 10);

        var energyCost = 1;
        ant.MoveTo(destination, energyCost);

        Assert.Equal(9, ant.Energy);
        Assert.Equal(destination, ant.Position);
    }

    [Fact]
    public void MoveTo_WhenAntEnergyReachesZero_ShouldEmitAntDiedEvent()
    {
        var startPosition = new Coordinate(0, 0);
        var destination = new Coordinate(1, 1);
        var ant = new Ant(startPosition, 1);

        var energyCost = 1;
        ant.MoveTo(destination, energyCost);

        Assert.NotEmpty(ant.DomainEvents);
        Assert.Single(ant.DomainEvents, new AntDied(ant.Id));
    }
}