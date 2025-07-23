using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChessGame.Domain;

public class Arrangement : ValueObject
{
    public enum MoveType
    {
        Move,
        Attack,
        None
    }

    public Arrangement()
    {
        Grid = new Piece[BoardSize, BoardSize];
        
        // Arrange white pieces
        Grid[0, 0] = new Piece(Piece.PieceType.Rook, Piece.PieceColor.White);
        Grid[0, 1] = new Piece(Piece.PieceType.Knight, Piece.PieceColor.White);
        Grid[0, 2] = new Piece(Piece.PieceType.Bishop, Piece.PieceColor.White);
        Grid[0, 3] = new Piece(Piece.PieceType.Queen, Piece.PieceColor.White);
        Grid[0, 4] = new Piece(Piece.PieceType.King, Piece.PieceColor.White);
        Grid[0, 5] = new Piece(Piece.PieceType.Bishop, Piece.PieceColor.White);
        Grid[0, 6] = new Piece(Piece.PieceType.Knight, Piece.PieceColor.White);
        Grid[0, 7] = new Piece(Piece.PieceType.Rook, Piece.PieceColor.White);

        // White pawns
        for (int col = 0; col < 8; col++)
        {
            Grid[1, col] = new Piece(Piece.PieceType.Pawn, Piece.PieceColor.White);
        }

        // Arrange black pieces
        Grid[7, 0] = new Piece(Piece.PieceType.Rook, Piece.PieceColor.Black);
        Grid[7, 1] = new Piece(Piece.PieceType.Knight, Piece.PieceColor.Black);
        Grid[7, 2] = new Piece(Piece.PieceType.Bishop, Piece.PieceColor.Black);
        Grid[7, 3] = new Piece(Piece.PieceType.Queen, Piece.PieceColor.Black);
        Grid[7, 4] = new Piece(Piece.PieceType.King, Piece.PieceColor.Black);
        Grid[7, 5] = new Piece(Piece.PieceType.Bishop, Piece.PieceColor.Black);
        Grid[7, 6] = new Piece(Piece.PieceType.Knight, Piece.PieceColor.Black);
        Grid[7, 7] = new Piece(Piece.PieceType.Rook, Piece.PieceColor.Black);

        // Black pawns
        for (int col = 0; col < 8; col++)
        {
            Grid[6, col] = new Piece(Piece.PieceType.Pawn, Piece.PieceColor.Black);
        }
    }

    public Arrangement(Arrangement oldState, Position tileFrom, Position tileTo)
    {
        Grid = new Piece[8, 8];
        var movedPiece = oldState.GetPieceAt(tileFrom);
        if (movedPiece == null) throw new ArgumentException("Dat kan niet");

        foreach (var (iPosition, iPiece) in oldState.GetAllPieces())
        {
            if (iPosition == tileFrom) continue;
            Grid[iPosition.X, iPosition.Y] = iPiece;
        }

        Grid[tileTo.X, tileTo.Y] = movedPiece;
    }

    public static int BoardSize => 8;

    public Piece?[,] Grid { get; }

    public IEnumerable<(Position position, Piece piece)> GetAllPieces()
    {
        foreach (var (position, piece) in GetAllPositions())
        {
            if (piece == null) continue;
            yield return (position, piece);
        }
    }

    public IEnumerable<(Position position, Piece? piece)> GetAllPositions()
    {
        for (var x = 0; x < Grid.GetLength(0); x++)
        for (var y = 0; y < Grid.GetLength(1); y++)
        {
            var position = new Position(x, y);

            yield return (position, GetPieceAt(position));
        }
    }

    public Piece? GetPieceAt(Position position)
    {
        return Grid[position.X, position.Y] ?? null;
    }

    public MoveSet GetMovesFrom(Position position)
    {
        List<Position> moveList = new();
        var piece = GetPieceAt(position);

        if (piece == null)
            return new MoveSet(position, piece, moveList);

        Debug.WriteLine($"Determine moveset for {position} - {piece} ");
        switch (piece.Type)
        {
            case Piece.PieceType.Rook:
                Debug.WriteLine(" > Moving x++");
                for (var x = position.X + 1; x < Grid.GetLength(0); x++)
                {
                    var targetPosition = new Position(x, position.Y);
                    var moveType = CheckMove(targetPosition, piece.Color);
                    if (moveType == MoveType.Move)
                    {
                        moveList.Add(targetPosition);
                    }
                    else if (moveType == MoveType.Attack)
                    {
                        moveList.Add(targetPosition);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                Debug.WriteLine(" > Moving x--");
                for (var x = position.X - 1; x >= 0; x--)
                {
                    var targetPosition = new Position(x, position.Y);
                    var moveType = CheckMove(targetPosition, piece.Color);
                    if (moveType == MoveType.Move)
                    {
                        moveList.Add(targetPosition);
                    }
                    else if (moveType == MoveType.Attack)
                    {
                        moveList.Add(targetPosition);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                Debug.WriteLine(" > Moving y++");
                for (var y = position.Y + 1; y < Grid.GetLength(1); y++)
                {
                    var targetPosition = new Position(position.X, y);
                    var moveType = CheckMove(targetPosition, piece.Color);
                    if (moveType == MoveType.Move)
                    {
                        moveList.Add(targetPosition);
                    }
                    else if (moveType == MoveType.Attack)
                    {
                        moveList.Add(targetPosition);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                Debug.WriteLine(" > Moving y--");
                for (var y = position.Y - 1; y >= 0; y--)
                {
                    var targetPosition = new Position(position.X, y);
                    var moveType = CheckMove(targetPosition, piece.Color);
                    if (moveType == MoveType.Move)
                    {
                        moveList.Add(targetPosition);
                    }
                    else if (moveType == MoveType.Attack)
                    {
                        moveList.Add(targetPosition);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                break;

            case Piece.PieceType.Pawn:
                if (piece.Color == Piece.PieceColor.White)
                {
                    Debug.WriteLine(" > Moving/attacking X+");
                    var targetPosition = new Position(position.X + 1, position.Y);
                    if (CheckMove(targetPosition, piece.Color) == MoveType.Move)
                        moveList.Add(targetPosition);

                    targetPosition = new Position(position.X + 1, position.Y + 1);
                    if (CheckMove(targetPosition, piece.Color) == MoveType.Attack)
                        moveList.Add(targetPosition);

                    targetPosition = new Position(position.X + 1, position.Y - 1);
                    if (CheckMove(targetPosition, piece.Color) == MoveType.Attack)
                        moveList.Add(targetPosition);
                        
                }
                else
                {
                    Debug.WriteLine(" > Moving/attacking X-");
                    var targetPosition = new Position(position.X - 1, position.Y);
                    if (CheckMove(targetPosition, piece.Color) == MoveType.Move)
                        moveList.Add(targetPosition);

                    targetPosition = new Position(position.X - 1, position.Y + 1);
                    if (CheckMove(targetPosition, piece.Color) == MoveType.Attack)
                        moveList.Add(targetPosition);

                    targetPosition = new Position(position.X - 1, position.Y - 1);
                    if (CheckMove(targetPosition, piece.Color) == MoveType.Attack)
                        moveList.Add(targetPosition);
                }
                break;

            case Piece.PieceType.Bishop:
                Debug.WriteLine(" > Moving diagonally");
                for (int dx = -1; dx <= 1; dx += 2)
                {
                    for (int dy = -1; dy <= 1; dy += 2)
                    {
                        int x = position.X + dx;
                        int y = position.Y + dy;
                        while (x >= 0 && y >= 0 && x < Grid.GetLength(0) && y < Grid.GetLength(1))
                        {
                            var targetPosition = new Position(x, y);
                            var moveType = CheckMove(targetPosition, piece.Color);
                            if (moveType == MoveType.Move)
                            {
                                moveList.Add(targetPosition);
                            }
                            else if (moveType == MoveType.Attack)
                            {
                                moveList.Add(targetPosition);
                                break;
                            }
                            else
                            {
                                break;
                            }
                            x += dx;
                            y += dy;
                        }
                    }
                }
                break;

            case Piece.PieceType.Knight:
                Debug.WriteLine(" > Moving in L-shape");
                var knightMoves = new (int dx, int dy)[]
                {
                    (2, 1), (1, 2), (-1, 2), (-2, 1),
                    (-2, -1), (-1, -2), (1, -2), (2, -1)
                };

                foreach (var (dx, dy) in knightMoves)
                {
                    var x = position.X + dx;
                    var y = position.Y + dy;
                    if (x >= 0 && y >= 0 && x < Grid.GetLength(0) && y < Grid.GetLength(1))
                    {
                        var targetPosition = new Position(x, y);
                        var moveType = CheckMove(targetPosition, piece.Color);
                        if (moveType != MoveType.None)
                        {
                            moveList.Add(targetPosition);
                        }
                    }
                }
                break;

            case Piece.PieceType.Queen:
                Debug.WriteLine(" > Moving like Rook and Bishop");
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0) continue;

                        int x = position.X + dx;
                        int y = position.Y + dy;
                        while (x >= 0 && y >= 0 && x < Grid.GetLength(0) && y < Grid.GetLength(1))
                        {
                            var targetPosition = new Position(x, y);
                            var moveType = CheckMove(targetPosition, piece.Color);
                            if (moveType == MoveType.Move)
                            {
                                moveList.Add(targetPosition);
                            }
                            else if (moveType == MoveType.Attack)
                            {
                                moveList.Add(targetPosition);
                                break;
                            }
                            else
                            {
                                break;
                            }
                            x += dx;
                            y += dy;
                        }
                    }
                }
                break;

            case Piece.PieceType.King:
                Debug.WriteLine(" > Moving one square in all directions");
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0) continue;

                        var x = position.X + dx;
                        var y = position.Y + dy;

                        if (x >= 0 && y >= 0 && x < Grid.GetLength(0) && y < Grid.GetLength(1))
                        {
                            var targetPosition = new Position(x, y);
                            var moveType = CheckMove(targetPosition, piece.Color);
                            if (moveType != MoveType.None)
                            {
                                moveList.Add(targetPosition);
                            }
                        }
                    }
                }
                break;

            default:
                throw new NotImplementedException();
        }

        return new MoveSet(position, piece, moveList);
    }

    private MoveType CheckMove(Position targetPosition, Piece.PieceColor moverColor)
    {
        Debug.Write($"   > {targetPosition}");
        if (!targetPosition.IsValid())
        {
            Debug.WriteLine(" - out of bounds");
            return MoveType.None;
        }

        var pieceAtTarget = GetPieceAt(targetPosition);
        Debug.Write($" has {pieceAtTarget?.ToString() ?? "nothing"}");

        if (pieceAtTarget == null)
        {
            Debug.WriteLine(" - is empty");
            return MoveType.Move;
        }

        if (pieceAtTarget.Color != moverColor)
        {
            Debug.WriteLine(" - has enemy");
            return MoveType.Attack;
        }

        Debug.WriteLine(" - can't move");
        return MoveType.None;
    }


    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }

}
