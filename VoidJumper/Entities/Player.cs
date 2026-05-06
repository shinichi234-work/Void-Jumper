namespace VoidJumper.Entities;

public class Player : Entity
{
    public int Score { get; set; }

    public Player()
    {
        Name = "Player";
        Health = 100;
    }
}
