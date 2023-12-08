namespace SprintPlanner.Infrastructure.MockProviderStrategy.Models;

[XmlRoot(ElementName = "project")]
public class MockProviderStrategyThreeResponse
{
    [XmlElement(ElementName = "tasks")] public MockProviderStrategyThreeTasksResponse Tasks { get; set; } = null!;
}

[XmlRoot(ElementName = "tasks")]
public class MockProviderStrategyThreeTasksResponse
{
    [XmlElement(ElementName = "task")] public List<MockProviderStrategyThreeTaskResponse> Task { get; set; } = null!;
}

[XmlRoot(ElementName = "task")]
public class MockProviderStrategyThreeTaskResponse
{
    [XmlElement(ElementName = "task_name")]
    public string Name { get; set; } = null!;

    [XmlElement(ElementName = "task_duration")]
    public int Duration { get; set; }

    [XmlElement(ElementName = "task_difficulty")]
    public int Difficulty { get; set; }
}