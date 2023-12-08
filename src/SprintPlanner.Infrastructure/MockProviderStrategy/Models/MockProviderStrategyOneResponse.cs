namespace SprintPlanner.Infrastructure.MockProviderStrategy.Models;

public class MockProviderStrategyOneResponse
{
    [JsonPropertyName("tasks")] public List<MockProviderStrategyOneTaskResponse> Tasks { get; set; } = null!;
}

public class MockProviderStrategyOneTaskResponse
{
    [JsonPropertyName("name")] public string Name { get; set; } = null!;

    [JsonPropertyName("duration")] public int Duration { get; set; }

    [JsonPropertyName("difficulty")] public int Difficulty { get; set; }
}