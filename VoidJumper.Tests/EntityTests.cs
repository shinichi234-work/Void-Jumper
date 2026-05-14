using VoidJumper.Entities;
using VoidJumper.Core;

namespace VoidJumper.Tests;

public class EntityTests
{
    [Fact]
    public void Player_DefaultName_ShouldBePlayer()
    {
        // Arrange
        var player = new Player();

        // Act
        var name = player.Name;

        // Assert
        Assert.Equal("Player", name);
    }

    [Fact]
    public void Player_DefaultHealth_ShouldBe100()
    {
        // Arrange
        var player = new Player();

        // Act
        var health = player.Health;

        // Assert
        Assert.Equal(100, health);
    }

    [Fact]
    public void TakeDamage_NormalAmount_ShouldReduceHealth()
    {
        // Arrange
        var player = new Player();

        // Act
        player.TakeDamage(30);

        // Assert
        Assert.Equal(70, player.Health);
    }

    [Fact]
    public void TakeDamage_MoreThanHealth_ShouldNotGoBelowZero()
    {
        // Arrange
        var player = new Player();

        // Act
        player.TakeDamage(200);

        // Assert
        Assert.Equal(0, player.Health);
    }

    [Fact]
    public void TakeDamage_ExactHealth_ShouldResultInZero()
    {
        // Arrange
        var player = new Player();

        // Act
        player.TakeDamage(100);

        // Assert
        Assert.Equal(0, player.Health);
    }

    [Fact]
    public void PatrolEnemy_DefaultStats_ShouldBeCorrect()
    {
        // Arrange
        var enemy = new PatrolEnemy();

        // Act
        var health = enemy.Health;
        var damage = enemy.Damage;

        // Assert
        Assert.Equal(60, health);
        Assert.Equal(8, damage);
    }

    [Fact]
    public void LevelBuilder_Build_ShouldReturnLevelWithCorrectValues()
    {
        // Arrange
        var builder = new LevelBuilder();

        // Act
        var level = builder
            .SetName("Test Level")
            .SetSize(80, 20)
            .SetEnemyCount(5)
            .SetHasBoss(true)
            .Build();

        // Assert
        Assert.Equal("Test Level", level.Name);
        Assert.Equal(80, level.Width);
        Assert.Equal(20, level.Height);
        Assert.Equal(5, level.EnemyCount);
        Assert.True(level.HasBoss);
    }
}
