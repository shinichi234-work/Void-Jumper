namespace VoidJumper.Entities;

public class JumpEnemy : Enemy
{
    public JumpEnemy()
    {
        Name = "Jumper";
        Health = 40;
        Damage = 15;
    }

    public override void Attack()
    {
        Console.WriteLine($"{Name} leaps from above and deals {Damage} damage");
    }
}
