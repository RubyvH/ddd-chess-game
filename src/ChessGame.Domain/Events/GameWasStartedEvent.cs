using System;
using ChessGame.Domain;

namespace DDD.Core;

public class GameWasStartedEvent(Guid id, PlayerWhite white, PlayerBlack black) : DomainEvent
{
    public Guid Id { get; } = id;
    public PlayerWhite White { get; } = white;
    public PlayerBlack Black { get; } = black;
}
