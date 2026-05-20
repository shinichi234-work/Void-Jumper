using VoidJumper.Core;

namespace VoidJumper.States;

public class MenuState : GameState
{
    public override void HandleInput(Game context, ConsoleKey key)
    {
        if (key == ConsoleKey.Enter)
            context.ChangeState(new PlayingState());
        else if (key == ConsoleKey.Escape)
            context.ChangeState(null);
    }

    public override void Update(Game context) { }

    public override void Draw(Game context)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("  +--------------------------------------------------+");
        Console.WriteLine("  |                                                  |");
        Console.WriteLine("  |           V O I D   J U M P E R                 |");
        Console.WriteLine("  |                                                  |");
        Console.WriteLine("  |         A turn-based dungeon explorer            |");
        Console.WriteLine("  |                                                  |");
        Console.WriteLine("  +--------------------------------------------------+");
        Console.WriteLine();
        Console.WriteLine("  > Enter  -  Start Game");
        Console.WriteLine("  > Esc    -  Quit");
        Console.WriteLine();
        Console.WriteLine("  Controls: W/A/S/D move  |  Space attack  |  H heal  |  Z undo");
    }
}
