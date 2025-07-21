using System;

namespace DDD.Core;

public class StartGameCommand(string player1Name, string player2Name, Guid player1Id, Guid player2Id) : IDomainCommand
{
    public string Player1Name { get; } = player1Name;
    public Guid Player1Id { get; } = player1Id;
    public string Player2Name { get; } = player2Name;
    public Guid Player2Id { get; } = player2Id;
}
