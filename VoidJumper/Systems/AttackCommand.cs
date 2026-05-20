using VoidJumper.Entities;

namespace VoidJumper.Systems;

public class AttackCommand : ICommand
{
    private readonly List<Enemy> _enemies;
    private readonly Player _player;
    private const int AttackDamage = 10;
    private int _previousHealth;
    private Enemy _target;

    public AttackCommand(List<Enemy> enemies) : this(enemies, null) { }

    public AttackCommand(List<Enemy> enemies, Player player)
    {
        _enemies = enemies;
        _player = player;
    }

    public void Execute()
    {
        _target = FindTarget();
        if (_target == null)
            return;
        _previousHealth = _target.Health;
        _target.TakeDamage(AttackDamage);
    }

    public void Undo()
    {
        if (_target == null)
            return;
        _target.Health = _previousHealth;
    }

    private Enemy FindTarget()
    {
        if (_player == null)
            return _enemies.Count > 0 ? _enemies[0] : null;
        Enemy nearest = null;
        int minDist = int.MaxValue;
        foreach (var e in _enemies)
        {
            if (e.Health <= 0)
                continue;
            int dist = Math.Abs(e.X - _player.X) + Math.Abs(e.Y - _player.Y);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = e;
            }
        }
        return nearest ?? (_enemies.Count > 0 ? _enemies[0] : null);
    }
}
