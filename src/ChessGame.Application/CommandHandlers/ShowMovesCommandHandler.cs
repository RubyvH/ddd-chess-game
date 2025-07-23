using System;
using System.Threading.Tasks;
using ChessGame.Domain;
using DDD.Core;

namespace ChessGame.Application.CommandHandlers;

public class ShowMovesCommandHandler(IGameRepository repository) : ICommandHandler
{
    private readonly IGameRepository _repository = repository;

    public async Task<string> Handle(ShowMovesCommand command)
    {
        var game = await _repository.GetGameById(command.GameId);
        var moves = game.GetMovesAsPlayer(command.PlayerId);


        foreach (var domainEvent in game.Events) Console.WriteLine(domainEvent);
        game.ClearEvents();
        await _repository.SaveGame(game);

        return string.Join(',', moves);
    }
}
