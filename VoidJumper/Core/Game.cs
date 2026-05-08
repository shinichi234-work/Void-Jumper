using VoidJumper.Entities;

namespace VoidJumper.Core;

public class Game
{
    private Level level;
    private Player player;
    private List<Enemy> enemies;

    public Game()
    {
        var gm = GameManager.Instance;
        level = new Level { Name = "Level 1", Width = gm.MapWidth, Height = gm.MapHeight };
        player = new Player();

        EnemyFactory[] factories = { new PatrolEnemyFactory(), new JumpEnemyFactory() };
        enemies = new List<Enemy>();
        foreach (var factory in factories)
            enemies.Add(factory.CreateEnemy());
    }

    public void Run()
    {
        bool running = true;
        while (running)
        {
            Update();
            Draw();

            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Escape)
                running = false;
        }
    }

    private void Update()
    {
        foreach (var enemy in enemies)
            enemy.Attack();
    }

    private void Draw()
    {
        Console.Clear();
        Console.WriteLine("=== Void Jumper ===");
        Console.WriteLine($"Level: {level.Name}  ({level.Width}x{level.Height})");
        Console.WriteLine($"Player: {player.Name}  HP: {player.Health}  Score: {player.Score}");
        Console.WriteLine($"Enemies on level: {enemies.Count}");
        Console.WriteLine("\nPress Esc to exit");
    }
}
