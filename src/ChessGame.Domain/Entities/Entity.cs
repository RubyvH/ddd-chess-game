namespace DDD.Core;

/// <summary>
///     Represents an Entity in the domain (DDD).
/// </summary>
/// <typeparam name="TId">The type of the Id of the entity.</typeparam>
public abstract class Entity<TId>
{
    public Entity(TId id)
    {
        Id = id;
    }

    public TId Id { get; }
}
