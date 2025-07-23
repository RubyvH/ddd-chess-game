using System;
using System.Collections.Generic;

namespace ChessGame.Domain;

public class Player(string name, Guid id, Piece.PieceColor color) : ValueObject
{
    public string Name { get; } = name;
    public Guid Id { get; } = id;
    public Piece.PieceColor Color { get; } = color;

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"{Name} ({Id}) as {Color}";
    }
}
