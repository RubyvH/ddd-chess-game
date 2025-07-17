using System.Collections.Generic;

namespace ChessGame.Domain;

public class Move : ValueObject {
    public Position From { get; }
    public Position To { get; };

    public Position(Position from, Position to)
    {
        From = from;
        To = to;
    }
}
