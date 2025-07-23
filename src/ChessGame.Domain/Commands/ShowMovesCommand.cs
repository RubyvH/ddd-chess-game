using System;

namespace DDD.Core;

public class ShowMovesCommand(Guid gameId, Guid playerId) : IDomainCommand
{
    public Guid GameId { get; } = gameId;
    public Guid PlayerId { get; } = playerId;
}
