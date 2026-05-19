using VoidJumper.Entities;

namespace VoidJumper.Systems;

public class MoveCommand : ICommand
{
    private readonly Player _player;
    private const int ScoreGain = 10;
    private int _previousScore;

    public MoveCommand(Player player)
    {
        _player = player;
    }

    public void Execute()
    {
        _previousScore = _player.Score;
        _player.Score += ScoreGain;
    }

    public void Undo()
    {
        _player.Score = _previousScore;
    }
}
