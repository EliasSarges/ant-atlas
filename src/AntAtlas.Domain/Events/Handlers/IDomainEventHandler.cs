namespace AntAtlas.Domain.Events.Handlers;

public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
{
    void Handle(TEvent domainEvent);
}