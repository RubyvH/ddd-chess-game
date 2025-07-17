using System.Collections.Generic;

namespace ChessGame.Domain;

public class Square : ValueObject
{
    public Piece? Piece { get; }
    public Position? Position { get; }

    public Square(Position? position, Piece? piece)
    {
        Position = position;
        Piece = piece;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Position;
        yield return Piece;
    }
}
