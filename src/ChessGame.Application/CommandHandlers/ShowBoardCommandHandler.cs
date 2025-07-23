using System;
using System.Threading.Tasks;
using ChessGame.Domain;
using DDD.Core;

namespace ChessGame.Application.CommandHandlers;

public class ShowBoardCommandHandler(IGameRepository repository) : ICommandHandler
{
    private readonly IGameRepository _repository = repository;

    public async Task<string> Handle(ShowBoardCommand command)
    {
        var game = await _repository.GetGameById(command.GameId);
        var boardView = game.GetBoardAsPlayer(command.PlayerId);


        foreach (var domainEvent in game.Events) Console.WriteLine(domainEvent);
        game.ClearEvents();

        await _repository.SaveGame(game);

        return boardView;
    }
}
