using System;
using System.Threading.Tasks;

namespace ChessGame.Domain;

public interface IGameRepository
{
    public Task<Game> GetGameById(Guid id);
    public Task SaveGame(Game game);
}
