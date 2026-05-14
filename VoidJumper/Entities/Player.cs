namespace VoidJumper.Entities;

public class Player : Entity
{
    private const int InitialHealth = 100;

    public int Score { get; set; }

    public event Action<int> OnHealthChanged;

    public Player()
    {
        Name = "Player";
        Health = InitialHealth;
        MaxHealth = InitialHealth;
    }

    public override void TakeDamage(int amount)
    {
        int before = Health;
        base.TakeDamage(amount);
        if (Health != before)
            OnHealthChanged?.Invoke(Health);
    }
}
