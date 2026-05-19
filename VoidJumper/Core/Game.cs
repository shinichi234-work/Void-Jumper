using VoidJumper.Entities;
using VoidJumper.States;
using VoidJumper.Systems;

namespace VoidJumper.Core;

public class Game
{
    private const int DefaultEnemyCount = 2;

    private readonly Level _level;
    private readonly Player _player;
    private readonly List<Enemy> _enemies;
    private readonly ConsoleHUD _hud;
    private GameState _currentState;

    public Level Level => _level;
    public Player Player => _player;
    public List<Enemy> Enemies => _enemies;
    public GameState CurrentState => _currentState;

    public Game()
    {
        _level = BuildLevel();
        _player = new Player();
        _enemies = SpawnEnemies();
        _hud = new ConsoleHUD(_player);
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
