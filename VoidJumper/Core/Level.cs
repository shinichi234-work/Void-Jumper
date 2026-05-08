namespace VoidJumper.Core;

public class Level
{
    public string Name { get; set; } = "";
    public int Width { get; set; }
    public int Height { get; set; }
    public int EnemyCount { get; set; }
    public bool HasBoss { get; set; }
}
