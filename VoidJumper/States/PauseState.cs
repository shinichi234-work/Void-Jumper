using VoidJumper.Core;

namespace VoidJumper.States;

public class PauseState : GameState
{
    public override void HandleInput(Game context, ConsoleKey key)
    {
        if (key == ConsoleKey.Escape)
            context.ChangeState(new PlayingState());
    }

    public override void Update(Game context) { }

    public override void Draw(Game context)
    {
        Console.Clear();
        Console.WriteLine("=== PAUSED ===");
        Console.WriteLine($"Player HP: {context.Player.Health}");
        Console.WriteLine("Press Esc to resume");
    }
}
