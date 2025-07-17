using System.Collections.Generic;

namespace ChessGame.Domain;

public class Position : ValueObject {
    public int X { get; }
    public int Y { get; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return X;
        yield return Y;
    }
}
