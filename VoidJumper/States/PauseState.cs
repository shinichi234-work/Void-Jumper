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
        Console.WriteLine();
        Console.WriteLine("  +--------------------------------------------------+");
        Console.WriteLine("  |                   P A U S E D                   |");
        Console.WriteLine("  +--------------------------------------------------+");
        Console.WriteLine();
        Console.WriteLine($"  {context.Level.Name}  |  Score: {context.Player.Score}");
        Console.WriteLine();
        int hp = context.Player.Health;
        int maxHp = context.Player.MaxHealth;
        int filled = maxHp > 0 ? (int)((double)hp / maxHp * 20) : 0;
        string bar = new string('#', filled) + new string('.', 20 - filled);
        Console.WriteLine($"  HP [{bar}] {hp}/{maxHp}");
        Console.WriteLine();
        Console.WriteLine("  > Esc  -  Resume Game");
    }
}
