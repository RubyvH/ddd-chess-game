using System;
using System.Collections.Generic;

namespace ChessGame.Domain;

public class PlayerWhite(string name, Guid id) : Player(name, id, Piece.PieceColor.White)
{
    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
