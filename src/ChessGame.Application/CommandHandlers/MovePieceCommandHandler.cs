using System;
using System.Threading.Tasks;
using ChessGame.Domain;
using DDD.Core;

namespace ChessGame.Application.CommandHandlers;

public class MovePieceCommandHandler(IGameRepository repository) : ICommandHandler
{
    private readonly IGameRepository _repository = repository;

    public async Task Handle(MovePieceCommand command)
    {
        var game = await _repository.GetGameById(command.GameId);
        game.Perform(command);
        foreach (var domainEvent in game.Events) Console.WriteLine(domainEvent);
        game.ClearEvents();
        await _repository.SaveGame(game);
    }
}
