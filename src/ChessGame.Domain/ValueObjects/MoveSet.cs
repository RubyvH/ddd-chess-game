using System.Collections.Generic;

namespace ChessGame.Domain;

public class MoveSet : ValueObject {
    public Position From { get; }
    public IEnumerable<Position> To { get; };

    public Position(Position from, IEnumerable<Position> to)
    {
        From = from;
        To = to;
    }
}
