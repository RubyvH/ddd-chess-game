using ChessGame.Domain;

namespace DDD.Core;

public class MovePieceCommand(Position from, Position to) : IDomainCommand
{
    public Position From { get; } = from;
    public Position to { get; } = to;
}
