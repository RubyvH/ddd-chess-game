using ChessGame.Domain;

namespace DDD.Core;

public class PlayerViewedBoardEvent(Player player) : DomainEvent
{
    public override string ToString()
    {
        return $"{OccurredOn} - Player {player} viewed the board";
    }
}
