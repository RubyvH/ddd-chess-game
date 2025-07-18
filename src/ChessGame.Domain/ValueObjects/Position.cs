using System.Collections.Generic;

namespace ChessGame.Domain;

public class Position(int x, int y) : ValueObject
{
    public int X { get; } = x;
    public int Y { get; } = y;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return X;
        yield return Y;
    }
}
