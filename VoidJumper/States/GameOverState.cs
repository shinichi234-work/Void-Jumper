using VoidJumper.Core;

namespace VoidJumper.States;

public class GameOverState : GameState
{
    private List<string> _topScores = new();

    public override void Enter(Game context)
    {
        context.SaveManager.AddScore(context.Player.Name, context.Player.Score);
        _topScores = context.SaveManager.GetTopScores();
    }

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
        Console.WriteLine("  |                G A M E   O V E R                 |");
        Console.WriteLine("  +--------------------------------------------------+");
        Console.WriteLine();
        Console.WriteLine($"  Score: {context.Player.Score}");
        Console.WriteLine();
        if (_topScores.Count > 0)
        {
            Console.WriteLine("  -- Top Scores --");
            foreach (var line in _topScores)
                Console.WriteLine($"  {line}");
            Console.WriteLine();
        }
        Console.WriteLine("  Press Enter or Esc to exit");
    }
}
