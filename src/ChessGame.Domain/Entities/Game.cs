


using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using DDD.Core;
using Microsoft.VisualBasic;

namespace ChessGame.Domain
{
    public class Game(Guid id): AggregateRoot<Guid>(id)
    {
        public Task StartGame(string player1Name, Guid player1Id, string player2Name, Guid player2Id)
        {
            var rand = new Random();
            bool player1IsWhite = rand.Next( 0, 1 ) > 0;
            Player1 = new Player(player1Name, player1IsWhite ? Piece.PieceColor.White : Piece.PieceColor.Black );
            Player2 = new Player(player2Name, player1IsWhite ? Piece.PieceColor.Black : Piece.PieceColor.White );

            GameState.Add(new Board());

            return Task.CompletedTask;
        } 
        
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public List<Board> GameState { get; set; }

        public Board GetBoard()
        {
            return GameState[GameState.Count - 1];
        }
        
    
        protected override void When(DomainEvent domainEvent)
        {
            throw new ArgumentException("Aggergates handle commands, not events!");
        }
    }
}