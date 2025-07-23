using System;

namespace ChessGame.Infrastructure.UserApi;

public record StartGameRequest(
    string Player1Name,
    string Player2Name,
    Guid Player1Id,
    Guid Player2Id
);
