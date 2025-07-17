using System.Collections.Generic;

namespace ChessGame.Domain;

public class MoveSet : ValueObject {
    public Position From { get; }
    public IEnumerable<Position> To { get; }

    public MoveSet(Position from, IEnumerable<Position> to)
    {
        From = from;
        To = to;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new System.NotImplementedException();
    }
}
