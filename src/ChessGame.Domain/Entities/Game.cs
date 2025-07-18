using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDD.Core;

namespace ChessGame.Domain;

public class Game(Guid id) : AggregateRoot<Guid>(id)
{
    private Player Player1 { get; set; }
    private Player Player2 { get; set; }
    private List<Board> GameState { get; set; }

    public Task StartGame(string player1Name, Guid player1Id, string player2Name, Guid player2Id)
    {
        var rand = new Random();
        var player1IsWhite = rand.Next(0, 1) > 0;
        Player1 = new Player(player1Name, player1IsWhite ? Piece.PieceColor.White : Piece.PieceColor.Black);
        Player2 = new Player(player2Name, player1IsWhite ? Piece.PieceColor.Black : Piece.PieceColor.White);

        GameState.Add(new Board());

        return Task.CompletedTask;
    }

    public Board GetBoard()
    {
        return GameState[GameState.Count - 1];
    }


    protected override void When(DomainEvent domainEvent)
    {
        throw new ArgumentException("Aggergates handle commands, not events!");
    }
}
