namespace DDD.Core;

/// <summary>
///     Represents a domain event (DDD) that should not be published publicly.
/// </summary>
public class InternalDomainEvent : DomainEvent
{
    public override string ToString()
    {
        return $"Domain Event: {GetType().Name}";
    }
}
