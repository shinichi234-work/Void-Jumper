using VoidJumper.Entities;
using VoidJumper.Systems;

namespace VoidJumper.Core;

public class Game
{
    private const int DefaultEnemyCount = 2;

    private Level level;
    private Player player;
    private List<Enemy> enemies;
    private ConsoleHUD hud;

    public Game()
    {
        level = BuildLevel();
        player = new Player();
        enemies = SpawnEnemies();
        hud = new ConsoleHUD(player);
    }

    private Level BuildLevel()
    {
        var gm = GameManager.Instance;
        return new LevelBuilder()
            .SetName("Level 1")
            .SetSize(gm.MapWidth, gm.MapHeight)
            .SetEnemyCount(DefaultEnemyCount)
            .SetHasBoss(false)
            .Build();
    }

    private List<Enemy> SpawnEnemies()
    {
        EnemyFactory[] factories = { new PatrolEnemyFactory(), new JumpEnemyFactory() };
        var list = new List<Enemy>();
        foreach (var factory in factories)
            list.Add(factory.CreateEnemy());
        return list;
    }

    public void Run()
    {
        var battle = new BattleFacade();
        battle.StartBattle(player);

        bool running = true;
        while (running)
        {
            Update();
            Draw();

            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Escape)
                running = false;
        }

        hud.Unsubscribe();
    }

    private void Update()
    {
        foreach (var enemy in enemies)
        {
            enemy.ExecuteStrategy();
            player.TakeDamage(enemy.Damage);
        }
    }

    private void Draw()
    {
        Console.Clear();
        Console.WriteLine("=== Void Jumper ===");
        Console.WriteLine($"Level: {level.Name}  ({level.Width}x{level.Height})");
        Console.WriteLine($"Enemies: {level.EnemyCount}  Boss: {level.HasBoss}");
        Console.WriteLine($"Player: {player.Name}  HP: {player.Health}  Score: {player.Score}");
        Console.WriteLine($"Enemies on level: {enemies.Count}");
        Console.WriteLine("\nPress Esc to exit");
    }
}
