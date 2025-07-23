using System;
using System.Threading.Tasks;
using ChessGame.Domain;
using DDD.Core;

namespace ChessGame.Application.CommandHandlers;

public class StartGameCommandHandler(IGameRepository repository) : ICommandHandler
{
    private readonly IGameRepository _repository = repository;

    public async Task<Guid> Handle(StartGameCommand command)
    {
        var game = Game.Create(command);
        await _repository.SaveGame(game);
        foreach (var domainEvent in game.Events) Console.WriteLine(domainEvent);
        game.ClearEvents();

        return game.Id;
    }
}
