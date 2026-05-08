namespace VoidJumper.Entities;

public class JumpEnemyFactory : EnemyFactory
{
    public override Enemy CreateEnemy()
    {
        return new JumpEnemy();
    }
}
