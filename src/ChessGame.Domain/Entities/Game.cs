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
        Player1 = new Player(command.Player1Name, command.Player1Id,
            player1IsWhite ? Piece.PieceColor.White : Piece.PieceColor.Black);
        Player2 = new Player(command.Player2Name, command.Player2Id,
            player1IsWhite ? Piece.PieceColor.Black : Piece.PieceColor.White);

        GameState =
        [
            new Board()
        ];
    }

    private Player Player1 { get; set; }
    private Player Player2 { get; set; }
    private List<Board> GameState { get; set; }

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
}
