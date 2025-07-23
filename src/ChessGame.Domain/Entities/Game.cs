using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDD.Core;

namespace ChessGame.Domain;

public class Game : AggregateRoot<Guid>
{
    public Game(StartGameCommand command) : base(Guid.NewGuid())
    {
        var rand = new Random();
        var player1IsWhite = rand.Next(0, 1) > 0;
        if (player1IsWhite)
        {
            White = new Player(command.Player1Name, command.Player1Id, Piece.PieceColor.White);
            Black = new Player(command.Player2Name, command.Player2Id, Piece.PieceColor.Black);
        }
        else
        {
            White = new Player(command.Player2Name, command.Player2Id, Piece.PieceColor.White);
            Black = new Player(command.Player1Name, command.Player1Id, Piece.PieceColor.Black);
        }

        GameState =
        [
            new Board()
        ];
    }

    private Player White { get; set; }
    private Player Black { get; set; }
    private List<Board> GameState { get; }

    public Task StartGame(string player1Name, Guid player1Id, string player2Name, Guid player2Id)
    {
        return Task.CompletedTask;
    }

    public Board GetBoard()
    {
        return GameState[^1];
    }


    protected override void When(DomainEvent domainEvent)
    {
        throw new ArgumentException("Aggergates handle commands, not events!");
    }

    public void PrintBoard()
    {
        Console.WriteLine("   1 2 3 4 5 6 7 8");
        for (var y = 0; y < GetBoard().Arrangement.Grid.GetLength(1); y++)
        {
            Console.Write($"{(char)('A' + y)}  ");
            for (var x = 0; x < GetBoard().Arrangement.Grid.GetLength(0); x++)
            {
                var position = new Position(x, y);
                var piece = GetBoard().Arrangement.GetPieceAt(position);
                if (piece == null)
                {
                    if (position.Color == Piece.PieceColor.White) Console.Write('■');
                    else Console.Write('□');
                }
                else
                {
                    if (piece.Color == Piece.PieceColor.White)
                        Console.Write(piece.Type.ToString().ToUpper()[0]);
                    else
                        Console.Write(piece.Type.ToString().ToLower()[0]);
                }

                Console.Write(' ');
            }

            Console.WriteLine();
        }
    }

    public void PrintMoveSets()
    {
        foreach (var moveSet in GetBoard().MoveSets)
            Console.WriteLine($"| {moveSet.Piece} ({moveSet.From}) can move to: {string.Join(',', moveSet.To)}");
    }

    public void Perform(MovePieceCommand command)
    {
        GameState.Add(new Board(GetBoard(), command.From, command.to));
    }
}
