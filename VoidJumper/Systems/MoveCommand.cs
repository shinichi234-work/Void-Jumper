using VoidJumper.Entities;

namespace VoidJumper.Systems;

public class MoveCommand : ICommand
{
    private readonly Player _player;
    private readonly char[,] _map;
    private readonly int _dx;
    private readonly int _dy;
    private const int ScoreGain = 10;
    private int _previousScore;
    private int _previousX;
    private int _previousY;

    public MoveCommand(Player player) : this(player, null, 1, 0) { }

    public MoveCommand(Player player, char[,] map, int dx, int dy)
    {
        _player = player;
        _map = map;
        _dx = dx;
        _dy = dy;
    }

    public void Execute()
    {
        _previousScore = _player.Score;
        _previousX = _player.X;
        _previousY = _player.Y;
        _player.Score += ScoreGain;
        if (_map != null)
        {
            int nx = _player.X + _dx;
            int ny = _player.Y + _dy;
            if (ny >= 0 && ny < _map.GetLength(0) && nx >= 0 && nx < _map.GetLength(1) && _map[ny, nx] != '#')
            {
                _player.X = nx;
                _player.Y = ny;
            }
        }
    }

    public void Undo()
    {
        _player.Score = _previousScore;
        _player.X = _previousX;
        _player.Y = _previousY;
    }
}
