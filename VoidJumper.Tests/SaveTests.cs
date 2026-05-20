using System.Text.Json;
using VoidJumper.Core;

namespace VoidJumper.Tests;

public class SaveTests
{
    [Fact]
    public void SaveData_Serialize_ShouldPreserveAllFields()
    {
        // Arrange
        var data = new SaveData { PlayerHealth = 75, PlayerScore = 150, LevelName = "Level 1", LevelIndex = 2, PlayerX = 5, PlayerY = 10 };

        // Act
        string json = JsonSerializer.Serialize(data);
        var loaded = JsonSerializer.Deserialize<SaveData>(json);

        // Assert
        Assert.Equal(data.PlayerHealth, loaded!.PlayerHealth);
        Assert.Equal(data.PlayerScore, loaded.PlayerScore);
        Assert.Equal(data.LevelName, loaded.LevelName);
        Assert.Equal(data.LevelIndex, loaded.LevelIndex);
        Assert.Equal(data.PlayerX, loaded.PlayerX);
        Assert.Equal(data.PlayerY, loaded.PlayerY);
    }

    [Fact]
    public void SaveData_Serialize_WhenZeroValues_ShouldRoundTrip()
    {
        // Arrange
        var data = new SaveData { PlayerHealth = 0, PlayerScore = 0, LevelName = "" };

        // Act
        string json = JsonSerializer.Serialize(data);
        var loaded = JsonSerializer.Deserialize<SaveData>(json);

        // Assert
        Assert.Equal(0, loaded!.PlayerHealth);
        Assert.Equal(0, loaded.PlayerScore);
    }

    [Fact]
    public void SaveData_Deserialize_ShouldReturnNotNull()
    {
        // Arrange
        var original = new SaveData { PlayerHealth = 42, PlayerScore = 999, LevelName = "Test" };
        string json = JsonSerializer.Serialize(original);

        // Act
        var restored = JsonSerializer.Deserialize<SaveData>(json);

        // Assert
        Assert.NotNull(restored);
        Assert.Equal(original.PlayerHealth, restored.PlayerHealth);
    }

    [Fact]
    public void SaveData_Serialize_ShouldProduceValidJson()
    {
        // Arrange
        var data = new SaveData { PlayerHealth = 100, PlayerScore = 0, LevelName = "Level 1" };

        // Act
        string json = JsonSerializer.Serialize(data);

        // Assert
        Assert.Contains("PlayerHealth", json);
        Assert.Contains("PlayerScore", json);
        Assert.Contains("LevelName", json);
    }
}
