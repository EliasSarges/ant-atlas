using AntAtlas.Domain.Events;

namespace AntAtlas.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; } = Guid.CreateVersion7();
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}