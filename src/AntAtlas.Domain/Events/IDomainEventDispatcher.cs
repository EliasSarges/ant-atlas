namespace AntAtlas.Domain.Events;

public interface IDomainEventDispatcher
{
    void Dispatch(IDomainEvent domainEvent);
}