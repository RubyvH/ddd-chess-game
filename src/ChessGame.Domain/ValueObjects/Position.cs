using System;
using System.Collections.Generic;

namespace ChessGame.Domain;

public class Position : ValueObject
{
    public Position(int x, int y)
    {
        Y = y;
        X = x;
    }

    public Position(string positionCode)
    {
        if (positionCode.Length != 2)
            throw new ArgumentException($"Position code '{positionCode}' is not valid, must be exactly 2 characters.");

        if (!char.IsBetween(positionCode.ToLower()[0], 'a', 'h'))
            throw new ArgumentException(
                $"Position code '{positionCode}' is not valid, first char must be between 'a' and 'h'.");

        if (!char.IsBetween(positionCode[1], '1', '8'))
            throw new ArgumentException(
                $"Position code '{positionCode}' is not valid, second char must be between '1' and '8'.");
        X = int.Parse(positionCode[1] + "")-1;
        Y = positionCode.ToLower()[0] - 'a';
    }

    public int X { get; }
    public int Y { get; }

    public Piece.PieceColor Color => (X + Y) % 2 == 0 ? Piece.PieceColor.Black : Piece.PieceColor.White;


    public override string ToString()
    {
        return $"{(char)('A' + Y)}{X + 1}";
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
