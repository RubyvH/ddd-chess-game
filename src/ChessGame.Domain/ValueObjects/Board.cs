using System.Collections.Generic;

namespace ChessGame.Domain;

public class Board : ValueObject
{
    public IReadOnlyList<Square> Squares { get; }

    public Board(IReadOnlyList<Square> squares)
    {
        Squares = squares;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        foreach (var square in Squares)
            yield return square;
    }
}
