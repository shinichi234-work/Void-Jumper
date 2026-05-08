using VoidJumper.Systems;

namespace VoidJumper.Core;

public class GameManager
{
    private static GameManager _instance;

    private GameManager()
    {
        MapWidth = 80;
        MapHeight = 20;
        Difficulty = Difficulty.Normal;
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameManager();
            return _instance;
        }
    }

    public int MapWidth { get; }
    public int MapHeight { get; }
    public Difficulty Difficulty { get; }

    public void Run()
    {
        Console.WriteLine($"Game started. Difficulty: {Difficulty}");

        IWeapon weapon = new SharpDecorator(new FireDecorator(new BaseSword(), 5), 3);
        Console.WriteLine($"Player weapon: {weapon.GetDescription()} ({weapon.GetDamage()} dmg)");

        new Game().Run();
    }
}
