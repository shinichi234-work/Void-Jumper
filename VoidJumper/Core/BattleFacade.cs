using VoidJumper.Entities;
using VoidJumper.Systems;

namespace VoidJumper.Core;

public class BattleFacade
{
    private readonly Level _level;
    private readonly List<Enemy> _enemies;
    private readonly IWeapon _weapon;

    public BattleFacade()
    {
        _level = new LevelBuilder()
            .SetName("Battle Arena")
            .SetSize(80, 20)
            .SetEnemyCount(3)
            .SetHasBoss(true)
            .Build();

        EnemyFactory[] factories = { new PatrolEnemyFactory(), new JumpEnemyFactory(), new PatrolEnemyFactory() };
        _enemies = new List<Enemy>();
        foreach (var f in factories)
            _enemies.Add(f.CreateEnemy());

        _weapon = new FireDecorator(new SharpDecorator(new BaseSword(), 3), 5);
    }

    public void StartBattle(Player player)
    {
        Console.WriteLine($"=== {_level.Name} | Boss: {_level.HasBoss} ===");
        Console.WriteLine($"{player.Name} | Weapon: {_weapon.GetDescription()} ({_weapon.GetDamage()} dmg)");
        foreach (var enemy in _enemies)
            enemy.Attack();
    }
}
