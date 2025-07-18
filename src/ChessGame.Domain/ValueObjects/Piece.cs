using System.Collections.Generic;

namespace ChessGame.Domain;

public class Piece(Piece.PieceType type, Piece.PieceColor color) : ValueObject
{
    public enum PieceColor
    {
        White,
        Black
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

    public PieceType Type { get; } = type;
    public PieceColor Color { get; } = color;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Type;
        yield return Color;
    }
}
