namespace VoidJumper.Entities;

public class Enemy : Entity
{
    public int Damage { get; set; }

    public Enemy(string name)
    {
        Name = name;
        Health = 50;
        Damage = 10;
    }
}
