using System;
using System.Collections.Generic;

namespace ChessGame.Domain;

public class PlayerBlack(string name, Guid id) : Player(name, id, Piece.PieceColor.Black)
{
    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
