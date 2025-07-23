using System;
using System.Collections.Generic;
using DDD.Core;

namespace ChessGame.Domain;

public class Game : AggregateRoot<Guid>
{
    private Game(Guid id, PlayerWhite white, PlayerBlack black) : base(id)
    {
        White = white;
        Black = black;

        BoardWasSeen = false;
        GameState.Add(new Board());
    }

    private PlayerWhite White { get; }
    private PlayerBlack Black { get; }
    private List<Board> GameState { get; } = [];

    public bool BoardWasSeen { get; set; }

    public static Game Create(StartGameCommand command)
    {
        PlayerWhite white;
        PlayerBlack black;

        var player1IsWhite = new Random().Next(0, 1) > 0;
        if (player1IsWhite)
        {
            white = new PlayerWhite(command.Player1Name, command.Player1Id);
            black = new PlayerBlack(command.Player2Name, command.Player2Id);
        }
        else
        {
            white = new PlayerWhite(command.Player2Name, command.Player2Id);
            black = new PlayerBlack(command.Player1Name, command.Player1Id);
        }

        var game = new Game(Guid.NewGuid(), white, black);

        game.RaiseEvent(new GameWasStartedEvent(game.Id, game.White, game.Black));
        return game;
    }


    public Board GetBoard()
    {
        return GameState[^1];
    }


    protected override void When(DomainEvent domainEvent)
    {
        Console.WriteLine($"Aggregate event: {domainEvent.GetType().Name}");
    }


    public void PrintMoveSets()
    {
        foreach (var moveSet in GetBoard().MoveSets)
            Console.WriteLine($"| {moveSet.Piece} ({moveSet.From}) can move to: {string.Join(',', moveSet.To)}");
    }

    public void Perform(MovePieceCommand command)
    {
        if (GetPlayerById(command.PlayerId).Color != GetBoard().ActiveColor)
        {
            Console.WriteLine("its not your turn!");
            return;
        }

        BoardWasSeen = false;
        GameState.Add(new Board(GetBoard(), command.From, command.To));
    }

    public string GetBoardAsPlayer(Guid playerId)
    {
        ViewAsPlayer(playerId);
        return PrintBoard();
    }

    private void ViewAsPlayer(Guid playerId)
    {
        var player = GetPlayerById(playerId);
        if (player.Color == GetBoard().ActiveColor && !BoardWasSeen) RaiseEvent(new PlayerViewedBoardEvent(player));
    }

    public Player GetPlayerById(Guid playerId)
    {
        if (White.Id == playerId) return White;
        if (Black.Id == playerId) return Black;
        throw new KeyNotFoundException($"Player {playerId} does not belong to this game");
    }

    private string PrintBoard()
    {
        var displayOut = "";
        displayOut += "   1 2 3 4 5 6 7 8";
        displayOut += "\n";
        for (var y = 0; y < GetBoard().Arrangement.Grid.GetLength(1); y++)
        {
            displayOut += $"{(char)('A' + y)}  ";
            for (var x = 0; x < GetBoard().Arrangement.Grid.GetLength(0); x++)
            {
                var position = new Position(x, y);
                var piece = GetBoard().Arrangement.GetPieceAt(position);
                if (piece == null)
                {
                    if (position.Color == Piece.PieceColor.White) Console.Write('■');
                    else
                        displayOut += '□';
                }
                else
                {
                    if (piece.Color == Piece.PieceColor.White)

                        displayOut += piece.Type.ToString().ToUpper()[0];
                    else
                        displayOut += piece.Type.ToString().ToLower()[0];
                }

                displayOut += ' ';
            }

            displayOut += "\n";
        }

        Console.WriteLine(displayOut);
        return displayOut;
    }

    public List<MoveSet> GetMovesAsPlayer(Guid playerId)
    {
        ViewAsPlayer(playerId);
        var moves = GetBoard().MoveSets;
        if (GetPlayerById(playerId).Color == GetBoard().ActiveColor) return moves;
        return new List<MoveSet>();
    }
}
