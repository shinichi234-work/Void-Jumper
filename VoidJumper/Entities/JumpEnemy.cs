namespace VoidJumper.Entities;

public class JumpEnemy : Enemy
{
    private const int InitialHealth = 40;
    private const int InitialDamage = 15;

    public JumpEnemy()
    {
        Name = "Jumper";
        Health = InitialHealth;
        Damage = InitialDamage;
    }

    public override void Attack()
    {
        Console.WriteLine($"{Name} leaps from above and deals {Damage} damage");
    }
}
