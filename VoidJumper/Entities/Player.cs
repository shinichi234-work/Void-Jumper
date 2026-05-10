namespace VoidJumper.Entities;

public class Player : Entity
{
    private const int InitialHealth = 100;

    public int Score { get; set; }

    public Player()
    {
        Name = "Player";
        Health = InitialHealth;
    }
}
