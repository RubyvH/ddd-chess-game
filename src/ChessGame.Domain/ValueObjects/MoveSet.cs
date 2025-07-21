using System;
using System.Collections.Generic;

namespace ChessGame.Domain;

public class MoveSet(Position from, Piece? piece, IEnumerable<Position> to) : ValueObject
{
    public Position From { get; } = from;
    public Piece? Piece { get; } = piece;
    public IEnumerable<Position> To { get; } = to;

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
