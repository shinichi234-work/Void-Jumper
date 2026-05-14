using VoidJumper.Entities;

namespace VoidJumper.Systems;

public class FleeBehavior : ICombatStrategy
{
    public void Execute(Enemy enemy)
    {
        Console.WriteLine($"{enemy.Name} retreats to avoid death");
    }
}
