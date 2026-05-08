namespace VoidJumper.Entities;

public class PatrolEnemy : Enemy
{
    public PatrolEnemy()
    {
        Name = "Patrol";
        Health = 60;
        Damage = 8;
    }

    public override void Attack()
    {
        Console.WriteLine($"{Name} moves along the platform and hits for {Damage} damage");
    }
}
