using VoidJumper.Entities;

namespace VoidJumper.Tests;

public class ObserverTests
{
    [Fact]
    public void OnHealthChanged_WhenTakeDamage_ShouldFireWithCurrentHealth()
    {
        // Arrange
        var player = new Player();
        int received = -1;
        player.OnHealthChanged += h => received = h;

        // Act
        player.TakeDamage(30);

        // Assert
        Assert.Equal(70, received);
    }

    [Fact]
    public void OnHealthChanged_WhenZeroDamage_ShouldNotFire()
    {
        // Arrange
        var player = new Player();
        bool fired = false;
        player.OnHealthChanged += _ => fired = true;

        // Act
        player.TakeDamage(0);

        // Assert
        Assert.False(fired);
    }

    [Fact]
    public void OnHealthChanged_AfterUnsubscribe_ShouldNotFire()
    {
        // Arrange
        var player = new Player();
        bool fired = false;
        void Handler(int h) => fired = true;
        player.OnHealthChanged += Handler;
        player.OnHealthChanged -= Handler;

        // Act
        player.TakeDamage(30);

        // Assert
        Assert.False(fired);
    }

    [Fact]
    public void OnHealthChanged_DamageExceedsHealth_ShouldFireWithZero()
    {
        // Arrange
        var player = new Player();
        int received = -1;
        player.OnHealthChanged += h => received = h;

        // Act
        player.TakeDamage(200);

        // Assert
        Assert.Equal(0, received);
    }
}
