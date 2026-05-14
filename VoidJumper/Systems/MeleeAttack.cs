using VoidJumper.Entities;

namespace VoidJumper.Systems;

public class MeleeAttack : ICombatStrategy
{
    public void Execute(Enemy enemy)
    {
        if (enemy.MaxHealth > 0 && (double)enemy.Health / enemy.MaxHealth < 0.2)
        {
            enemy.SetStrategy(new FleeBehavior());
            return;
        }
        Console.WriteLine($"{enemy.Name} closes in and strikes for {enemy.Damage} damage");
    }
}
