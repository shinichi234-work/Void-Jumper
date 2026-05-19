using VoidJumper.Entities;

namespace VoidJumper.Systems;

public class AttackCommand : ICommand
{
    private readonly List<Enemy> _enemies;
    private const int AttackDamage = 10;
    private int _previousHealth;

    public AttackCommand(List<Enemy> enemies)
    {
        _enemies = enemies;
    }

    public void Execute()
    {
        if (_enemies.Count == 0)
            return;
        _previousHealth = _enemies[0].Health;
        _enemies[0].TakeDamage(AttackDamage);
    }

    public void Undo()
    {
        if (_enemies.Count == 0)
            return;
        _enemies[0].Health = _previousHealth;
    }
}
