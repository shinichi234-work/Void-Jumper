namespace VoidJumper.Entities;

public abstract class Enemy : Entity
{
    public int Damage { get; set; }

    public abstract void Attack();
}
