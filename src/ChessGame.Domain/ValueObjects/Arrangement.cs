using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChessGame.Domain;

public class Arrangement : ValueObject {
    public Piece[, ] Grid { get;}

    public Arrangement()
    {
        Grid = new Piece[8, 8];
        // arrange board
        Grid[0, 0] = new Piece(Piece.PieceType.Rook, Piece.PieceColor.White);
        Grid[0, 7] = new Piece(Piece.PieceType.Rook, Piece.PieceColor.White);
        Grid[1, 0] = new Piece(Piece.PieceType.Pawn, Piece.PieceColor.White);
        Grid[1, 7] = new Piece(Piece.PieceType.Pawn, Piece.PieceColor.White);
        // todo, implement rest

        Grid[7, 0] = new Piece(Piece.PieceType.Rook, Piece.PieceColor.Black);
        Grid[7, 7] = new Piece(Piece.PieceType.Rook, Piece.PieceColor.Black);
        Grid[6, 0] = new Piece(Piece.PieceType.Pawn, Piece.PieceColor.Black);
        Grid[6, 7] = new Piece(Piece.PieceType.Pawn, Piece.PieceColor.Black);
    }
    public Arrangement(Arrangement oldState, Position tileFrom, Position tileTo)
    {
        Grid = new Piece[8, 8];
        var movedPiece = oldState.GetPieceAt(tileFrom);
        if(movedPiece == null) throw new System.ArgumentException("Dat kan niet");

        foreach (var (iPosition, iPiece) in oldState.GetAllPieces())
        {
            if (iPosition == tileFrom) continue;
            if (iPiece != null) Grid[iPosition.X, iPosition.Y] = iPiece;
        }

        Grid[tileTo.X, tileTo.Y] = movedPiece;
    }
    
    public IEnumerable<(Position position, Piece? piece)> GetAllPieces()
    {
        for (int x = 0; x < Grid.GetLength(0); x++)
        {
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                var position = new Position(x, y);
                yield return (position, GetPieceAt(position));
            }
        }
    }

    public Piece? GetPieceAt(Position position)
    {
        return Grid[position.X, position.Y] ?? null;
    }

    public MoveSet GetMovesFrom(Position position)
    {
        // retrieve piece at position
        // determine logic for piece movement
        // determine logic for piece combat
        // collect all possible moves in list
        // return list as moveset
        List<Position> moveList = new();
        var piece = GetPieceAt(position);

        if (piece == null)
            return new MoveSet(position, moveList);
            
        switch (piece.Type)
        {
            case Piece.PieceType.Rook:

                for (int x = position.X + 1; x < Grid.GetLength(0); x++)
                {
                    var targetPosition = new Position(x, position.Y);
                    var pieceAtTarget = GetPieceAt(targetPosition);
                    
                    if (pieceAtTarget == null)
                        moveList.Add(targetPosition);

                    else if (pieceAtTarget.Color != piece.Color)
                    {
                        moveList.Add(targetPosition);
                        break;
                    }
                    else
                        break;
                }
                for (int x = position.X - 1; x > 0; x--)
                {
                    var targetPosition = new Position(x, position.Y);
                    var pieceAtTarget = GetPieceAt(targetPosition);
                    
                    if (pieceAtTarget == null)
                        moveList.Add(targetPosition);

                    else if (pieceAtTarget.Color != piece.Color)
                    {
                        moveList.Add(targetPosition);
                        break;
                    }
                    else
                        break;
                }
                for (int y = position.Y + 1; y < Grid.GetLength(0); y++)
                {
                    var targetPosition = new Position(position.X, y);
                    var pieceAtTarget = GetPieceAt(targetPosition);
                    
                    if (pieceAtTarget == null)
                        moveList.Add(targetPosition);

                    else if (pieceAtTarget.Color != piece.Color)
                    {
                        moveList.Add(targetPosition);
                        break;
                    }
                    else
                        break;
                }
                for (int y = position.Y - 1; y > 0; y--)
                {
                    var targetPosition = new Position(position.X, y);
                    var pieceAtTarget = GetPieceAt(targetPosition);
                    
                    if (pieceAtTarget == null)
                        moveList.Add(targetPosition);

                    else if (pieceAtTarget.Color != piece.Color)
                    {
                        moveList.Add(targetPosition);
                        break;
                    }
                    else
                        break;
                }
                break;

            case Piece.PieceType.Pawn:
                if (piece.Color == Piece.PieceColor.White)
                {
                    moveList.Add(new Position(position.X + 1, position.Y));
                    if (GetPieceAt(new Position(position.X + 1, position.Y + 1))?.Color != piece.Color)
                        moveList.Add(new Position(position.X + 1, position.Y + 1));

                    if (GetPieceAt(new Position(position.X + 1, position.Y - 1))?.Color != piece.Color)
                        moveList.Add(new Position(position.X + 1, position.Y - 1));


                }
                else
                {
                    moveList.Add(new Position(position.X - 1, position.Y));
                    if (GetPieceAt(new Position(position.X - 1, position.Y + 1))?.Color != piece.Color)
                        moveList.Add(new Position(position.X - 1, position.Y + 1));

                    if (GetPieceAt(new Position(position.X - 1, position.Y - 1))?.Color != piece.Color)
                        moveList.Add(new Position(position.X - 1, position.Y - 1));

                }

                break;

        }
        return new MoveSet(position, moveList);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new System.NotImplementedException();
    }
}
