using AntAtlas.Domain.Entities;

namespace AntAtlas.Domain.Events.Handlers;

public class AntDiedEventHandler : IDomainEventHandler<AntDied>
{
    private readonly Colony _colony;

    public AntDiedEventHandler(Colony colony)
    {
        _colony = colony;
    }

    public void Handle(AntDied domainEvent)
    {
        _colony.DecreaseAntAlivesCounter();
    }
}