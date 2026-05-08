namespace VoidJumper.Core;

public class LevelBuilder
{
    private readonly Level _level = new Level();

    public LevelBuilder SetName(string name)
    {
        _level.Name = name;
        return this;
    }

    public LevelBuilder SetSize(int width, int height)
    {
        _level.Width = width;
        _level.Height = height;
        return this;
    }

    public LevelBuilder SetEnemyCount(int count)
    {
        _level.EnemyCount = count;
        return this;
    }

    public LevelBuilder SetHasBoss(bool hasBoss)
    {
        _level.HasBoss = hasBoss;
        return this;
    }

    public Level Build()
    {
        return _level;
    }
}
