namespace VoidJumper.Entities;

public class PatrolEnemyFactory : EnemyFactory
{
    public override Enemy CreateEnemy()
    {
        return new PatrolEnemy();
    }
}
