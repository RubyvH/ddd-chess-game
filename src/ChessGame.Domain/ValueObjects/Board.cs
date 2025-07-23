using System;
using System.Collections.Generic;

namespace ChessGame.Domain;

public class Board : ValueObject
{
    public Board()
    {
        Arrangement = new Arrangement();
        MoveSets = DetermineMoveSets();
    }

    public Board(Board previousBoard, Position tileFrom, Position tileTo)
    {
        Arrangement = new Arrangement(previousBoard.Arrangement, tileFrom, tileTo);

        MoveSets = DetermineMoveSets();

        ActiveColor = previousBoard.ActiveColor == Piece.PieceColor.White
            ? Piece.PieceColor.Black
            : Piece.PieceColor.White;
    }

    public Arrangement Arrangement { get; }
    public List<MoveSet> MoveSets { get; }

    public Piece.PieceColor ActiveColor { get; set; }

    private List<MoveSet> DetermineMoveSets()
    {
        var moveSets = new List<MoveSet>();
        foreach (var (position, piece) in Arrangement.GetAllPieces())
            if (piece.Color == ActiveColor)
                moveSets.Add(Arrangement.GetMovesFrom(position));
        return moveSets;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
