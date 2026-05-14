using VoidJumper.Systems;

namespace VoidJumper.Entities;

public abstract class Enemy : Entity
{
    public int Damage { get; set; }
    public ICombatStrategy Strategy { get; private set; } = new MeleeAttack();

    public abstract void Attack();

    public void SetStrategy(ICombatStrategy strategy)
    {
        Strategy = strategy;
    }

    public void ExecuteStrategy()
    {
        Strategy.Execute(this);
    }
}
