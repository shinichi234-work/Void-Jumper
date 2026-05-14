using VoidJumper.Entities;
using VoidJumper.Systems;

namespace VoidJumper.Tests;

public class StrategyTests
{
    [Fact]
    public void PatrolEnemy_DefaultStrategy_ShouldBeMeleeAttack()
    {
        // Arrange
        var enemy = new PatrolEnemy();

        // Act
        var strategy = enemy.Strategy;

        // Assert
        Assert.IsType<MeleeAttack>(strategy);
    }

    [Fact]
    public void SetStrategy_ToRanged_ShouldUpdateStrategy()
    {
        // Arrange
        var enemy = new PatrolEnemy();
        var ranged = new RangedAttack();

        // Act
        enemy.SetStrategy(ranged);

        // Assert
        Assert.IsType<RangedAttack>(enemy.Strategy);
    }

    [Fact]
    public void ExecuteStrategy_WhenHealthCritical_ShouldSwitchToFlee()
    {
        // Arrange
        var enemy = new PatrolEnemy();
        enemy.TakeDamage(52);

        // Act
        enemy.ExecuteStrategy();

        // Assert
        Assert.IsType<FleeBehavior>(enemy.Strategy);
    }

    [Fact]
    public void ExecuteStrategy_WhenHealthNormal_ShouldKeepMeleeAttack()
    {
        // Arrange
        var enemy = new PatrolEnemy();
        enemy.TakeDamage(10);

        // Act
        enemy.ExecuteStrategy();

        // Assert
        Assert.IsType<MeleeAttack>(enemy.Strategy);
    }
}
