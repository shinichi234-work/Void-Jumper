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
        Console.WriteLine("=== Void Jumper ===");
        Console.WriteLine("Press Enter to start");
        Console.WriteLine("Press Esc to quit");
    }
}
