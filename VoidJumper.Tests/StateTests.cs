using VoidJumper.Core;
using VoidJumper.States;

namespace VoidJumper.Tests;

public class StateTests
{
    [Fact]
    public void MenuState_HandleInput_Enter_ShouldTransitionToPlaying()
    {
        // Arrange
        var game = new Game();
        var state = new MenuState();
        game.ChangeState(state);

        // Act
        state.HandleInput(game, ConsoleKey.Enter);

        // Assert
        Assert.IsType<PlayingState>(game.CurrentState);
    }

    [Fact]
    public void PlayingState_HandleInput_Escape_ShouldTransitionToPause()
    {
        // Arrange
        var game = new Game();
        var state = new PlayingState();
        game.ChangeState(state);

        // Act
        state.HandleInput(game, ConsoleKey.Escape);

        // Assert
        Assert.IsType<PauseState>(game.CurrentState);
    }

    [Fact]
    public void PauseState_HandleInput_Escape_ShouldTransitionToPlaying()
    {
        // Arrange
        var game = new Game();
        var state = new PauseState();
        game.ChangeState(state);

        // Act
        state.HandleInput(game, ConsoleKey.Escape);

        // Assert
        Assert.IsType<PlayingState>(game.CurrentState);
    }

    [Fact]
    public void PlayingState_Update_WhenPlayerDead_ShouldTransitionToGameOver()
    {
        // Arrange
        var game = new Game();
        var state = new PlayingState();
        game.ChangeState(state);
        game.Player.TakeDamage(1000);

        // Act
        state.Update(game);

        // Assert
        Assert.IsType<GameOverState>(game.CurrentState);
    }

    [Fact]
    public void MenuState_HandleInput_Escape_ShouldExitGame()
    {
        // Arrange
        var game = new Game();
        var state = new MenuState();
        game.ChangeState(state);

        // Act
        state.HandleInput(game, ConsoleKey.Escape);

        // Assert
        Assert.Null(game.CurrentState);
    }
}
