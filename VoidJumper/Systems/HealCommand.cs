using VoidJumper.Entities;

namespace VoidJumper.Systems;

public class HealCommand : ICommand
{
    private readonly Player _player;
    private const int HealAmount = 20;
    private int _previousHealth;

    public HealCommand(Player player)
    {
        _player = player;
    }

    public void Execute()
    {
        _previousHealth = _player.Health;
        _player.Health = Math.Min(_player.Health + HealAmount, _player.MaxHealth);
    }

    public void Undo()
    {
        _player.Health = _previousHealth;
    }
}
