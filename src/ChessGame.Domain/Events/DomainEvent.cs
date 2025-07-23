using System;

namespace DDD.Core;

/// <summary>
///     Represents a domain event in the system.
/// </summary>
public abstract class DomainEvent
{
    protected DomainEvent()
    {
        OccurredOn = DateTime.UtcNow;
    }

    public DateTime OccurredOn { get; }

    public abstract override string ToString();
}
