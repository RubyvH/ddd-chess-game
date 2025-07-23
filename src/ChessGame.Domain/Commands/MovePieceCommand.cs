using System;
using ChessGame.Domain;

namespace DDD.Core;

public class MovePieceCommand(Guid gameId, Guid playerId, Position from, Position to) : IDomainCommand
{
    public Guid GameId { get; } = gameId;
    public Guid PlayerId { get; } = playerId;
    public Position From { get; } = from;
    public Position To { get; } = to;
}
