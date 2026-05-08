namespace VoidJumper.Entities;

public class Entity
{
    public string Name { get; set; } = "";
    public int Health { get; set; }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0)
            Health = 0;
    }
}
