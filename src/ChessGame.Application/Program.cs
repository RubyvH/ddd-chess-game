using System;
using System.Linq;
using ChessGame.Domain;
using DDD.Core;

internal class Program
{
    private static void Main(string[] args)
    {
        var startCommand = new StartGameCommand("Henk", "Ook Henk", Guid.NewGuid(), Guid.NewGuid());
        var theGame = new Game(startCommand);

        while (theGame.GetBoard().MoveSets.Any())
        {
            Console.Clear();
            theGame.PrintBoard();
            Console.WriteLine();
            theGame.PrintMoveSets();
            Console.WriteLine();

            var validPieceSelected = false;
            var validMoveSelected = false;

            MoveSet moveSet;
            Piece pieceToMove;
            Position fromPosition;
            while (!validPieceSelected)
            {
                Console.WriteLine("Select a piece to move...");
                var positionCodeToMoveFrom = Console.ReadLine();
                if (positionCodeToMoveFrom is null) continue;


                try
                {
                    fromPosition = new Position(positionCodeToMoveFrom);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                pieceToMove = theGame.GetBoard().Arrangement.GetPieceAt(fromPosition);
                if (pieceToMove is null) Console.WriteLine($"No piece found at {fromPosition}.");

                moveSet = theGame.GetBoard().Arrangement.
                validPieceSelected = true;
            }

            while (!validMoveSelected)
            {
                Console.WriteLine("Select a position to move to ()...");
                var positionCodeToMoveFrom = Console.ReadLine();
                if (positionCodeToMoveFrom is null) continue;

                Position targetPosition = null;

                try
                {
                    targetPosition = new Position(positionCodeToMoveFrom);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                pieceToMove = theGame.GetBoard().Arrangement.GetPieceAt(targetPosition);
                if (pieceToMove is null) Console.WriteLine($"No piece found at {targetPosition}.");
            }
        }
    }
}
