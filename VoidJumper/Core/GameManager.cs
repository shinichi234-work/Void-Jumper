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
        new Game().Run();
    }
}
