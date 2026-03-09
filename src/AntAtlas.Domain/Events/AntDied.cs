namespace AntAtlas.Domain.Events;

public record AntDied(Guid Id) : IDomainEvent;