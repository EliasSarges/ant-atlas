namespace AntAtlas.Domain.Exceptions;

public class AntIsDeadException(Guid id)
    : InvalidOperationException($"The ant {id} is dead and cannot perform this action.");