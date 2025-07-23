using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessGame.Domain;

public class FakeRepository : IGameRepository
{
    private readonly Dictionary<Guid, Game> db = new();

    public async Task<Game> GetGameById(Guid id)
    {
        if (db.ContainsKey(id)) return db[id];
        throw new KeyNotFoundException($"Requested game with {id} was not found");
    }

    public Task SaveGame(Game game)
    {
        db[game.Id] = game;
        return Task.CompletedTask;
    }
}
