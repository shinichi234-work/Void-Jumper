using VoidJumper.Entities;
using VoidJumper.Systems;

namespace VoidJumper.Tests;

public class CommandTests
{
    [Fact]
    public void MoveCommand_Execute_ShouldIncreaseScore()
    {
        // Arrange
        var player = new Player();
        var command = new MoveCommand(player);

        // Act
        command.Execute();

        // Assert
        Assert.Equal(10, player.Score);
    }

    [Fact]
    public void MoveCommand_Undo_ShouldRestoreScore()
    {
        // Arrange
        var player = new Player();
        var command = new MoveCommand(player);
        command.Execute();

        // Act
        command.Undo();

        // Assert
        Assert.Equal(0, player.Score);
    }

    [Fact]
    public void AttackCommand_Execute_ShouldDamageEnemy()
    {
        // Arrange
        var enemy = new PatrolEnemy();
        var command = new AttackCommand(new List<Enemy> { enemy });

        // Act
        command.Execute();

        // Assert
        Assert.Equal(50, enemy.Health);
    }

    [Fact]
    public void AttackCommand_Undo_ShouldRestoreEnemyHealth()
    {
        // Arrange
        var enemy = new PatrolEnemy();
        var command = new AttackCommand(new List<Enemy> { enemy });
        command.Execute();

        // Act
        command.Undo();

        // Assert
        Assert.Equal(60, enemy.Health);
    }

    [Fact]
    public void InputManager_Handle_ShouldExecuteBoundCommand()
    {
        // Arrange
        var player = new Player();
        var manager = new InputManager();
        manager.Bind(ConsoleKey.W, new MoveCommand(player));

        // Act
        manager.Handle(ConsoleKey.W);

        // Assert
        Assert.Equal(10, player.Score);
    }

    [Fact]
    public void InputManager_Undo_ShouldReverseLastCommand()
    {
        // Arrange
        var player = new Player();
        var manager = new InputManager();
        manager.Bind(ConsoleKey.W, new MoveCommand(player));
        manager.Handle(ConsoleKey.W);

        // Act
        manager.Undo();

        // Assert
        Assert.Equal(0, player.Score);
    }
}
