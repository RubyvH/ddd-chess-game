using System.Collections.Generic;

namespace ChessGame.Domain;

public class Player : ValueObject {
    public string Name { get; }
    public Piece.PieceColor Color { get; }

    public Player(string name, Piece.PieceColor color)
    {
        Name = name;
        Color = color;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new System.NotImplementedException();
    }
}
