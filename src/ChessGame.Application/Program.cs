using System;
using ChessGame.Domain;
using DDD.Core;

internal class Program
{
    private static void Main(string[] args)
    {
        var startCommand = new StartGameCommand("Henk", "Ook Henk", Guid.NewGuid(), Guid.NewGuid());
        var theGame = new Game(startCommand);
        theGame.PrintBoard();
        theGame.PrintMoveSets();
    }
}
