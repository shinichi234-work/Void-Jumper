namespace VoidJumper.Entities;

public class Entity
{
    public string Name { get; set; } = "";
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public virtual void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0)
            Health = 0;
    }
}
