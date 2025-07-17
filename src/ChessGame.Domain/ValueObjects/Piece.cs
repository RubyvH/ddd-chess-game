using System.Collections.Generic;

namespace ChessGame.Domain;

public class Piece : ValueObject
{
    public PieceType Type { get; }
    public PieceColor Color { get; }

    public Piece(PieceType type, PieceColor color)
    {
        Type = type;
        Color = color;
    }

    public enum PieceType
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King
    }

    public enum PieceColor
    {
        White,
        Black
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Type;
        yield return Color;
    }
}
