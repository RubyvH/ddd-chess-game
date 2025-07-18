using System.Collections.Generic;
using System.Linq;

namespace ChessGame.Domain;

/// <summary>
///     Represents a ValueObject in the domain (DDD).
/// </summary>
public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetAtomicValues();

    public override bool Equals(object? obj)
    {
        return obj != null &&
               GetType() == obj.GetType() &&
               AreEqual(this, (ValueObject)obj);
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;

        return AreEqual(left, right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        if (left is null && right is null) return false;
        if (left is null || right is null) return true;

        return !AreEqual(left, right);
    }

    private static bool AreEqual(ValueObject left, ValueObject right)
    {
        if (left is null)
            return right is null;
        return right is object &&
               left.GetAtomicValues().SequenceEqual(right.GetAtomicValues());
    }
}
