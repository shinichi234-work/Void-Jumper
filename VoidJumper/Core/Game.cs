using VoidJumper.Entities;
using VoidJumper.States;
using VoidJumper.Systems;

namespace VoidJumper.Core;

public class Game
{
    public const int TotalLevels = 5;
    private const int PlayerStartX = 3;
    private const int PlayerStartY = 10;

    private Level _level;
    private List<Enemy> _enemies;
    private char[,] _map;
    private InputManager _inputManager;

    private readonly Player _player;
    private readonly ConsoleHUD _hud;
    private readonly SaveManager _saveManager;
    private GameState _currentState;

    public int LevelIndex { get; private set; }
    public bool TurnPassed { get; set; }

    public Level Level => _level;
    public Player Player => _player;
    public List<Enemy> Enemies => _enemies;
    public GameState CurrentState => _currentState;
    public InputManager InputManager => _inputManager;
    public SaveManager SaveManager => _saveManager;
    public char[,] Map => _map;

    public Game()
    {
        _player = new Player { X = PlayerStartX, Y = PlayerStartY };
        _hud = new ConsoleHUD(_player);
        _saveManager = new SaveManager();
        LoadLevel(0);
    }

    private void LoadLevel(int index)
    {
        LevelIndex = index;
        _map = CreateMap(index);
        _level = BuildLevel(index);
        _enemies = SpawnEnemies(index);
        _inputManager = BuildInputManager();
        _player.X = PlayerStartX;
        _player.Y = PlayerStartY;
    }

    public void AdvanceLevel()
    {
        LoadLevel(LevelIndex + 1);
    }

    private static char[,] CreateMap(int levelIndex)
    {
        const int rows = 14;
        const int cols = 60;
        var map = new char[rows, cols];
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                map[r, c] = '.';
        for (int c = 0; c < cols; c++) { map[0, c] = '#'; map[rows - 1, c] = '#'; }
        for (int r = 0; r < rows; r++) { map[r, 0] = '#'; map[r, cols - 1] = '#'; }

        if (levelIndex < 2)
        {
            PlaceWall(map, 6, 19, 4);
            PlaceWall(map, 6, 44, 4);
        }
        else if (levelIndex < 4)
        {
            PlaceWall(map, 3, 5, 6);
            PlaceWall(map, 6, 21, 7);
            PlaceWall(map, 9, 39, 7);
            PlaceWall(map, 11, 49, 5);
        }
        else
        {
            PlaceWall(map, 2, 15, 6);
            PlaceWall(map, 4, 35, 7);
            PlaceWall(map, 6, 10, 7);
            PlaceWall(map, 6, 43, 7);
            PlaceWall(map, 9, 25, 7);
            PlaceWall(map, 11, 5, 7);
        }
        return map;
    }

    private static void PlaceWall(char[,] map, int row, int colStart, int length)
    {
        for (int c = colStart; c < colStart + length; c++)
            map[row, c] = '#';
    }

    private Level BuildLevel(int index)
    {
        var gm = GameManager.Instance;
        return new LevelBuilder()
            .SetName($"Level {index + 1}")
            .SetSize(gm.MapWidth, gm.MapHeight)
            .SetEnemyCount(GetEnemySpawns(index).Count)
            .SetHasBoss(index == TotalLevels - 1)
            .Build();
    }

    private List<Enemy> SpawnEnemies(int index)
    {
        var spawns = GetEnemySpawns(index);
        var list = new List<Enemy>();
        foreach (var (x, y, isJumper) in spawns)
        {
            Enemy enemy = isJumper
                ? new JumpEnemyFactory().CreateEnemy()
                : new PatrolEnemyFactory().CreateEnemy();
            enemy.X = x;
            enemy.Y = y;
            list.Add(enemy);
        }
        return list;
    }

    private static List<(int x, int y, bool isJumper)> GetEnemySpawns(int levelIndex)
    {
        switch (levelIndex)
        {
            case 0:
                return new List<(int, int, bool)> { (30, 10, false), (50, 10, true) };
            case 1:
                return new List<(int, int, bool)> { (20, 10, false), (42, 10, false), (55, 3, true) };
            case 2:
                return new List<(int, int, bool)> { (25, 10, false), (45, 10, false), (30, 5, true), (50, 5, true) };
            case 3:
                return new List<(int, int, bool)> { (15, 10, false), (30, 10, false), (50, 10, false), (25, 5, true), (48, 5, true) };
            default:
                return new List<(int, int, bool)> { (15, 10, false), (30, 10, false), (48, 10, false), (20, 5, true), (38, 5, true), (55, 5, true) };
        }
    }

    private InputManager BuildInputManager()
    {
        var manager = new InputManager();
        manager.Bind(ConsoleKey.W, new MoveCommand(_player, _map, 0, -1));
        manager.Bind(ConsoleKey.A, new MoveCommand(_player, _map, -1, 0));
        manager.Bind(ConsoleKey.S, new MoveCommand(_player, _map, 0, 1));
        manager.Bind(ConsoleKey.D, new MoveCommand(_player, _map, 1, 0));
        manager.Bind(ConsoleKey.Spacebar, new AttackCommand(_enemies, _player));
        manager.Bind(ConsoleKey.H, new HealCommand(_player));
        return manager;
    }

    public void ChangeState(GameState state)
    {
        _currentState = state;
        _currentState?.Enter(this);
    }

    public void Run()
    {
        ChangeState(new MenuState());

        while (_currentState != null)
        {
            _currentState.Draw(this);
            var key = Console.ReadKey(true).Key;
            _currentState.HandleInput(this, key);
            _currentState?.Update(this);
        }

        _hud.Unsubscribe();
    }
}
