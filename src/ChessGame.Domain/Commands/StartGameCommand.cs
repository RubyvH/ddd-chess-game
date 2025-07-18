using System;

namespace DDD.Core;

public class StartGameCommand(string player1Name, string player2Name, Guid player1Id, Guid player2Id) : IDomainCommand
{
    private string Player1Name { get; } = player1Name;
    private Guid Player1Id { get; } = player1Id;
    private string Player2Name { get; } = player2Name;
    private Guid Player2Id { get; } = player2Id;

}
