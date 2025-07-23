using System;
using ChessGame.Application.CommandHandlers;
using ChessGame.Domain;
using ChessGame.Infrastructure.UserApi;
using DDD.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

// Program.cs (top-level statements)
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IGameRepository, FakeRepository>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/", () => "Hello, welcome to chess game!");
app.MapGet("/{gameId}/{playerId}", async (Guid gameId, Guid playerId, IGameRepository repository) =>
{
    var commandHandler = new ShowBoardCommandHandler(repository);
    var boardState = await commandHandler.Handle(new ShowBoardCommand(gameId, playerId));
    return boardState;
});

app.MapPost("/NewGame", async (StartGameRequest request, IGameRepository repository) =>
{
    var commandHandler = new StartGameCommandHandler(repository);
    var gameId = await commandHandler.Handle(new StartGameCommand(request.Player1Name, request.Player2Name,
        request.Player1Id,
        request.Player2Id));
    return gameId;
});


app.Run();


/*
internal class Program
{
    private static void Main(string[] args)
    {
        var startCommand = new StartGameCommand("Henk", "Ook Henk", Guid.NewGuid(), Guid.NewGuid());
        var theGame = Game.Create(startCommand);

        while (theGame.GetBoard().MoveSets.Any())
        {
            Console.Clear();
            theGame.PrintBoard();
            Console.WriteLine();
            theGame.PrintMoveSets();
            Console.WriteLine();


            MoveSet? moveSet = null;
            while (moveSet is null)
            {
                Console.WriteLine("");
                Console.WriteLine("Select a piece to move...");
                var positionCodeToMoveFrom = Console.ReadLine();
                if (positionCodeToMoveFrom is null) continue;


                Position fromPosition;
                try
                {
                    fromPosition = new Position(positionCodeToMoveFrom);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                moveSet = theGame.GetBoard().Arrangement.GetMovesFrom(fromPosition);

                if (moveSet.Piece is null)
                {
                    Console.WriteLine($"No piece found at {moveSet.From}.");
                    moveSet = null;
                }
                else if (!moveSet.To.Any())
                {
                    Console.WriteLine($"{moveSet.Piece} at {moveSet.From} has no moves");
                    moveSet = null;
                }
            }

            Position? targetPosition = null;
            while (targetPosition is null)
            {
                Console.WriteLine("");
                Console.WriteLine($"Select a position to move {moveSet.Piece} to {string.Join(',', moveSet.To)}...");
                var positionCodeToMoveTo = Console.ReadLine();
                if (positionCodeToMoveTo is null) continue;

                try
                {
                    targetPosition = new Position(positionCodeToMoveTo);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                if (!moveSet.To.Contains(targetPosition))
                {
                    Console.WriteLine($"{moveSet.Piece} at {moveSet.From} can't move to {targetPosition}.");
                    targetPosition = null;
                }
            }

            var moveCommand = new MovePieceCommand(moveSet.From, targetPosition);
            theGame.Perform(moveCommand);
        }
    }
}
*/
