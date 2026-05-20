namespace VoidJumper.Entities;

public class JumpEnemy : Enemy
{
    private const int InitialHealth = 40;
    private const int InitialDamage = 15;

    public JumpEnemy()
    {
        Name = "Jumper";
        Health = InitialHealth;
        MaxHealth = InitialHealth;
        Damage = InitialDamage;
    }

    public override void Attack() { }
}
