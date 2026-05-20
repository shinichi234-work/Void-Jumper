using VoidJumper.Core;
using VoidJumper.Entities;
using VoidJumper.Systems;

namespace VoidJumper.States;

public class PlayingState : GameState
{
    private string _statusMessage = "";

    public override void HandleInput(Game context, ConsoleKey key)
    {
        if (key == ConsoleKey.Escape)
        {
            context.ChangeState(new PauseState());
            return;
        }
        if (key == ConsoleKey.Z)
        {
            context.InputManager.Undo();
            return;
        }
        if (key == ConsoleKey.F5)
        {
            var data = new SaveData
            {
                PlayerHealth = context.Player.Health,
                PlayerScore = context.Player.Score,
                LevelName = context.Level.Name,
                LevelIndex = context.LevelIndex,
                PlayerX = context.Player.X,
                PlayerY = context.Player.Y
            };
            context.SaveManager.Save(data);
            _statusMessage = "Game saved.";
            return;
        }
        if (key == ConsoleKey.F9)
        {
            var data = context.SaveManager.Load();
            if (data != null)
            {
                context.LoadLevel(data.LevelIndex);
                context.Player.Health = data.PlayerHealth;
                context.Player.Score = data.PlayerScore;
                context.Player.X = data.PlayerX;
                context.Player.Y = data.PlayerY;
                _statusMessage = "Game loaded.";
            }
            else
            {
                _statusMessage = "Save file not found.";
            }
            return;
        }
        context.InputManager.Handle(key);
        context.TurnPassed = true;
    }

    public override void Update(Game context)
    {
        if (context.TurnPassed)
        {
            context.TurnPassed = false;
            RunEnemyTurns(context);

            if (context.Enemies.Count > 0 && context.Enemies.All(e => e.Health <= 0))
            {
                if (context.LevelIndex >= Game.TotalLevels - 1)
                    context.ChangeState(new VictoryState());
                else
                    context.AdvanceLevel();
                return;
            }
        }

        if (context.Player.Health <= 0)
            context.ChangeState(new GameOverState());
    }

    private static void RunEnemyTurns(Game context)
    {
        foreach (var enemy in context.Enemies)
        {
            if (enemy.Health <= 0)
                continue;
            enemy.ExecuteStrategy();
            int px = context.Player.X;
            int py = context.Player.Y;
            int dist = Math.Abs(enemy.X - px) + Math.Abs(enemy.Y - py);
            if (enemy.Strategy is FleeBehavior)
            {
                MoveEnemy(context, enemy, -Math.Sign(px - enemy.X), -Math.Sign(py - enemy.Y));
            }
            else if (dist <= 1)
            {
                context.Player.TakeDamage(enemy.Damage);
            }
            else
            {
                int steps = enemy is JumpEnemy ? 2 : 1;
                for (int i = 0; i < steps; i++)
                    MoveEnemy(context, enemy, Math.Sign(px - enemy.X), Math.Sign(py - enemy.Y));
            }
        }
    }

    private static void MoveEnemy(Game context, Enemy enemy, int dx, int dy)
    {
        if (dx == 0 && dy == 0)
            return;
        int nx = enemy.X + dx;
        int ny = enemy.Y + dy;
        if (ny >= 0 && ny < context.Map.GetLength(0) &&
            nx >= 0 && nx < context.Map.GetLength(1) &&
            context.Map[ny, nx] != '#')
        {
            enemy.X = nx;
            enemy.Y = ny;
        }
    }

    public override void Draw(Game context)
    {
        Console.Clear();
        int mapRows = context.Map.GetLength(0);
        int mapCols = context.Map.GetLength(1);
        var grid = new char[mapRows, mapCols];
        for (int r = 0; r < mapRows; r++)
            for (int c = 0; c < mapCols; c++)
                grid[r, c] = context.Map[r, c];
        foreach (var enemy in context.Enemies)
        {
            if (enemy.Health > 0 && InBounds(grid, enemy.X, enemy.Y))
                grid[enemy.Y, enemy.X] = enemy is JumpEnemy ? 'J' : 'P';
        }
        if (InBounds(grid, context.Player.X, context.Player.Y))
            grid[context.Player.Y, context.Player.X] = '@';
        Console.WriteLine($"  {context.Level.Name}  |  Level {context.LevelIndex + 1}/{Game.TotalLevels}");
        Console.WriteLine();
        for (int r = 0; r < mapRows; r++)
        {
            Console.Write("  ");
            for (int c = 0; c < mapCols; c++)
                Console.Write(grid[r, c]);
            Console.WriteLine();
        }
        Console.WriteLine();
        int hp = context.Player.Health;
        int maxHp = context.Player.MaxHealth;
        int filled = maxHp > 0 ? (int)((double)hp / maxHp * 20) : 0;
        string bar = new string('#', filled) + new string('.', 20 - filled);
        int alive = context.Enemies.Count(e => e.Health > 0);
        Console.WriteLine($"  HP [{bar}] {hp}/{maxHp}   Score: {context.Player.Score}   Enemies: {alive}");
        if (!string.IsNullOrEmpty(_statusMessage))
        {
            Console.WriteLine($"  {_statusMessage}");
            _statusMessage = "";
        }
        else
        {
            Console.WriteLine();
        }
        Console.WriteLine("  W/A/S/D move  |  Space attack  |  H heal  |  Z undo  |  Esc pause  |  F5 save  |  F9 load");
    }

    private static bool InBounds(char[,] grid, int x, int y)
    {
        return y >= 0 && y < grid.GetLength(0) && x >= 0 && x < grid.GetLength(1);
    }
}
