using VoidJumper.Core;

namespace VoidJumper.States;

public class GameOverState : GameState
{
    public override void HandleInput(Game context, ConsoleKey key)
    {
        if (key == ConsoleKey.Enter || key == ConsoleKey.Escape)
            context.ChangeState(null);
    }

    public override void Update(Game context) { }

    public override void Draw(Game context)
    {
        Console.Clear();
        Console.WriteLine("=== GAME OVER ===");
        Console.WriteLine($"Score: {context.Player.Score}");
        Console.WriteLine("Press Enter or Esc to exit");
    }
}
