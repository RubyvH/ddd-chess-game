using System.Collections.Generic;

namespace ChessGame.Domain;

public class Position(int x, int y) : ValueObject
{
    public int X { get; } = x;
    public int Y { get; } = y;

    public Piece.PieceColor Color()
    {
        if ((X + Y) % 2 == 0) return Piece.PieceColor.Black;
        return Piece.PieceColor.White;
    }

    public override string ToString()
    {
        return $"{(char)('A' + Y)}{X+1}";
    }

    public bool IsValid()
    {
        if (X < 0 || X > Arrangement.BoardSize - 1 ||
            Y < 0 || Y > Arrangement.BoardSize - 1) return false;
        return true;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return X;
        yield return Y;
    }
}
