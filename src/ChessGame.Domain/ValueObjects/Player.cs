using System;
using System.Collections.Generic;

namespace ChessGame.Domain;

public class Player(string name, Piece.PieceColor color) : ValueObject
{
    public string Name { get; } = name;
    public Piece.PieceColor Color { get; } = color;

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
