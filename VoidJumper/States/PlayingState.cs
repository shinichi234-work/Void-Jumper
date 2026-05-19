using VoidJumper.Core;

namespace VoidJumper.States;

public class PlayingState : GameState
{
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
                LevelName = context.Level.Name
            };
            context.SaveManager.Save(data);
            Console.WriteLine("Game saved.");
            return;
        }
        if (key == ConsoleKey.F9)
        {
            var data = context.SaveManager.Load();
            if (data != null)
            {
                context.Player.Health = data.PlayerHealth;
                context.Player.Score = data.PlayerScore;
            }
            return;
        }
        context.InputManager.Handle(key);
    }

    public override void Update(Game context)
    {
        foreach (var enemy in context.Enemies)
        {
            enemy.ExecuteStrategy();
            context.Player.TakeDamage(enemy.Damage);
        }

        if (context.Player.Health <= 0)
            context.ChangeState(new GameOverState());
    }

    public override void Draw(Game context)
    {
        Console.Clear();
        Console.WriteLine("=== Void Jumper ===");
        Console.WriteLine($"Level: {context.Level.Name}  ({context.Level.Width}x{context.Level.Height})");
        Console.WriteLine($"Player: {context.Player.Name}  HP: {context.Player.Health}  Score: {context.Player.Score}");
        Console.WriteLine($"Enemies: {context.Enemies.Count}");
        Console.WriteLine("\nW - move  Space - attack  H - heal  Z - undo  F5 - save  F9 - load  Esc - pause");
    }
}
