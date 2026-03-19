using AntAtlas.Domain.Entities;
using AntAtlas.Domain.Events;
using AntAtlas.Domain.Events.Handlers;
using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Tests.IntegrationTests;

public class AntDeathEventIntegrationTest
{
    [Fact]
    public void AntDeathEvent_WhenIsTriggered_ShouldDecreaseColonyAntsAliveCounter()
    {
        var colony = new Colony(foodStock: new FoodStock(10), foodCost: 2);
        var eventDispatcher = new InMemoryDomainEventDispatcher();
        var antDiedEventHandler = new AntDiedEventHandler(colony);

        eventDispatcher.Subscribe(typeof(AntDied), antDiedEventHandler);

        var antPosition = new Coordinate(0, 0);
        const int energy = 1;

        Assert.True(colony.TrySpawnAnt(antPosition, energy, out var aliveAnt));
        Assert.True(colony.TrySpawnAnt(antPosition, energy, out var deadAnt));
        Assert.NotNull(deadAnt);

        deadAnt.MoveTo(newPosition: new Coordinate(1, 1), 1);

        var antEvents = deadAnt.DomainEvents;

        foreach (var antEvent in antEvents)
        {
            eventDispatcher.Dispatch(antEvent);
        }

        deadAnt.ClearDomainEvents();

        Assert.Equal(1, colony.AntsAliveCount);
    }
}