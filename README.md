# DDD Chess Game

A simple .NET 9 chess game project demonstrating Domain-Driven Design (DDD) principles.

## Project Structure

```
src/
├── ChessGame.Domain/           # Domain layer - Business logic and entities
│   ├── Entities/
│   └── Repositories/
├── ChessGame.Application/      # Application layer - Use cases and services
│   └── Services/
├── ChessGame.Infrastructure/   # Infrastructure layer - Data access
│   └── Repositories/
```

## Getting Started

1. Build the solution:

   ```bash
   dotnet build
   ```

2. Run the application:

   ```bash
   dotnet run --project src/ChessGame.Application
   ```

3. Explore the application functionality via the console output.
