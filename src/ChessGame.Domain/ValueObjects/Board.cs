using System.Collections.Generic;
using System.Drawing;

namespace ChessGame.Domain;

public class Board : ValueObject
{
    public Arrangement arrangement { get; }
    public List<MoveSet> moveSets { get; }

    public Piece.PieceColor activeColor { get; }

    public Board()
    {
        arrangement = new Arrangement();
        moveSets = DetermineMoveSets();
    }
    public Board(Board previousBoard, Position tileFrom, Position tileTo)
    {

        arrangement = new Arrangement(previousBoard.arrangement, tileFrom, tileTo);

        moveSets = DetermineMoveSets();

    }

    private List<MoveSet> DetermineMoveSets()
    {

        var moveSets = new List<MoveSet>();
        foreach (var (position, piece) in arrangement.GetAllPieces())
        {
            if (piece != null && piece.Color == activeColor)
                moveSets.Add(arrangement.GetMovesFrom(position));
        }
        return moveSets;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new System.NotImplementedException();
    }
}
