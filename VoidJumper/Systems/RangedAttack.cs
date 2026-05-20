using VoidJumper.Entities;

namespace VoidJumper.Systems;

public class RangedAttack : ICombatStrategy
{
    public void Execute(Enemy enemy)
    {
        if (enemy.MaxHealth > 0 && (double)enemy.Health / enemy.MaxHealth < 0.2)
            enemy.SetStrategy(new FleeBehavior());
    }
}
