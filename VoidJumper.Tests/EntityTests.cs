using VoidJumper.Entities;
using VoidJumper.Core;

namespace VoidJumper.Tests;

public class EntityTests
{
    [Fact]
    public void Player_DefaultName_ShouldBePlayer()
    {
        var player = new Player();

        Assert.Equal("Player", player.Name);
    }

    [Fact]
    public void Player_DefaultHealth_ShouldBe100()
    {
        var player = new Player();

        Assert.Equal(100, player.Health);
    }

    [Fact]
    public void TakeDamage_NormalAmount_ShouldReduceHealth()
    {
        var player = new Player();

        player.TakeDamage(30);

        Assert.Equal(70, player.Health);
    }

    [Fact]
    public void TakeDamage_MoreThanHealth_ShouldNotGoBelowZero()
    {
        var player = new Player();

        player.TakeDamage(200);

        Assert.Equal(0, player.Health);
    }

    [Fact]
    public void TakeDamage_ExactHealth_ShouldResultInZero()
    {
        var player = new Player();

        player.TakeDamage(100);

        Assert.Equal(0, player.Health);
    }

    [Fact]
    public void PatrolEnemy_DefaultStats_ShouldBeCorrect()
    {
        var enemy = new PatrolEnemy();

        Assert.Equal(60, enemy.Health);
        Assert.Equal(8, enemy.Damage);
    }

    [Fact]
    public void LevelBuilder_Build_ShouldReturnLevelWithCorrectValues()
    {
        var level = new LevelBuilder()
            .SetName("Test Level")
            .SetSize(80, 20)
            .SetEnemyCount(5)
            .SetHasBoss(true)
            .Build();

        Assert.Equal("Test Level", level.Name);
        Assert.Equal(80, level.Width);
        Assert.Equal(20, level.Height);
        Assert.Equal(5, level.EnemyCount);
        Assert.True(level.HasBoss);
    }
}
