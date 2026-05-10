namespace VoidJumper.Entities;

public class PatrolEnemy : Enemy
{
    private const int InitialHealth = 60;
    private const int InitialDamage = 8;

    public PatrolEnemy()
    {
        Name = "Patrol";
        Health = InitialHealth;
        Damage = InitialDamage;
    }

    public override void Attack()
    {
        Console.WriteLine($"{Name} moves along the platform and hits for {Damage} damage");
    }
}
