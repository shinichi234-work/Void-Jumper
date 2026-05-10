using VoidJumper.Entities;
using VoidJumper.Systems;

namespace VoidJumper.Core;

public class BattleFacade
{
    private const int ArenaWidth = 80;
    private const int ArenaHeight = 20;
    private const int ArenaEnemyCount = 3;
    private const int FireBonus = 5;
    private const int SharpBonus = 3;

    private readonly Level _level;
    private readonly List<Enemy> _enemies;
    private readonly IWeapon _weapon;

    public BattleFacade()
    {
        _level = BuildArena();
        _enemies = SpawnEnemies();
        _weapon = EquipWeapon();
    }

    private Level BuildArena() => new LevelBuilder()
        .SetName("Battle Arena")
        .SetSize(ArenaWidth, ArenaHeight)
        .SetEnemyCount(ArenaEnemyCount)
        .SetHasBoss(true)
        .Build();

    private List<Enemy> SpawnEnemies()
    {
        EnemyFactory[] factories = { new PatrolEnemyFactory(), new JumpEnemyFactory(), new PatrolEnemyFactory() };
        var list = new List<Enemy>();
        foreach (var factory in factories)
            list.Add(factory.CreateEnemy());
        return list;
    }

    private IWeapon EquipWeapon() => new FireDecorator(new SharpDecorator(new BaseSword(), SharpBonus), FireBonus);

    public void StartBattle(Player player)
    {
        Console.WriteLine($"=== {_level.Name} | Boss: {_level.HasBoss} ===");
        Console.WriteLine($"{player.Name} | Weapon: {_weapon.GetDescription()} ({_weapon.GetDamage()} dmg)");
        foreach (var enemy in _enemies)
            enemy.Attack();
    }
}
