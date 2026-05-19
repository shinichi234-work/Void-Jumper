using System.Text.Json;

namespace VoidJumper.Core;

public class SaveManager
{
    private static readonly string SavePath = "save.json";
    private static readonly string ScoresPath = "scores.txt";

    public void Save(SaveData data)
    {
        string json = JsonSerializer.Serialize(data);
        File.WriteAllText(SavePath, json);
    }

    public SaveData? Load()
    {
        if (!File.Exists(SavePath))
            return null;
        try
        {
            string json = File.ReadAllText(SavePath);
            return JsonSerializer.Deserialize<SaveData>(json);
        }
        catch
        {
            return null;
        }
    }

    public void AddScore(string name, int score)
    {
        File.AppendAllText(ScoresPath, $"{score} {name}{Environment.NewLine}");
    }

    public List<string> GetTopScores(int count = 5)
    {
        if (!File.Exists(ScoresPath))
            return new List<string>();
        try
        {
            return File.ReadAllLines(ScoresPath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Trim())
                .OrderByDescending(line => int.TryParse(line.Split(' ')[0], out int s) ? s : 0)
                .Take(count)
                .ToList();
        }
        catch
        {
            return new List<string>();
        }
    }
}
