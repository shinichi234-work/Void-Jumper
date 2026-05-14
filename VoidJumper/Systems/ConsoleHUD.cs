using VoidJumper.Entities;

namespace VoidJumper.Systems;

public class ConsoleHUD
{
    private readonly Player _player;

    public ConsoleHUD(Player player)
    {
        _player = player;
        _player.OnHealthChanged += DisplayHealth;
    }

    public void Unsubscribe()
    {
        _player.OnHealthChanged -= DisplayHealth;
    }

    private void DisplayHealth(int currentHealth)
    {
        int max = _player.MaxHealth;
        int filled = max > 0 ? (int)((double)currentHealth / max * 10) : 0;
        string bar = new string('|', filled) + new string('.', 10 - filled);
        Console.WriteLine($"[HP: {bar}] {currentHealth}/{max}");
    }
}
