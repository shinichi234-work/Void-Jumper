using VoidJumper.Core;

namespace VoidJumper.States;

public class VictoryState : GameState
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
        Console.WriteLine();
        Console.WriteLine("  +--------------------------------------------------+");
        Console.WriteLine("  |                  V I C T O R Y                   |");
        Console.WriteLine("  +--------------------------------------------------+");
        Console.WriteLine();
        Console.WriteLine($"  You conquered all {Game.TotalLevels} levels of the Void!");
        Console.WriteLine($"  Final Score: {context.Player.Score}");
        Console.WriteLine($"  HP remaining: {context.Player.Health}/{context.Player.MaxHealth}");
        Console.WriteLine();
        Console.WriteLine("  Press Enter or Esc to exit");
    }
}