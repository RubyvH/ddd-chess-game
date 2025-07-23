using System;

namespace DDD.Core;

public class ShowBoardCommand(Guid gameId, Guid playerId) : IDomainCommand
{
    public Guid GameId { get; } = gameId;
    public Guid PlayerId { get; } = playerId;
}
