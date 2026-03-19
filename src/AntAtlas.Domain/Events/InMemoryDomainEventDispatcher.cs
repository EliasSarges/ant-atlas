namespace AntAtlas.Domain.Events;

public class InMemoryDomainEventDispatcher : IDomainEventDispatcher
{
    private Dictionary<Type, List<object>> _events = new();

    public void Subscribe(Type eventType, object handler)
    {
        if (!_events.TryGetValue(eventType, out var handlers))
        {
            handlers = new List<object>();
            _events.Add(eventType, handlers);
        }

        handlers.Add(handler);
    }

    public void Dispatch(IDomainEvent domainEvent)
    {
        if (!_events.TryGetValue(domainEvent.GetType(), out var handlers)) return;

        foreach (var handler in handlers)
        {
            ((dynamic)handler).Handle((dynamic)domainEvent);
        }
    }
}