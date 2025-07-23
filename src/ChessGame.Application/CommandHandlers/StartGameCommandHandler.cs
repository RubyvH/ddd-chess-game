using System;
using System.Threading.Tasks;
using ChessGame.Domain;
using DDD.Core;

namespace ChessGame.Application.CommandHandlers;

public class StartGameCommandHandler(IGameRepository repository)
{
    private readonly IGameRepository _repository = repository;

    public Task Handle(StartGameCommand command)
    {
        var game = Game.Create(command);
        _repository.SaveGame(game);
        foreach (var domainEvent in game.Events) Console.WriteLine(domainEvent);
        game.ClearEvents();

        return Task.CompletedTask;
    }
}
