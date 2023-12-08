namespace SprintPlanner.Infrastructure.MockProviderStrategy.Models;

public class TaskResponse
{
    public string Name { get; init; } = null!;
    public int Difficulty { get; init; }
    public int Duration { get; init; }

    public override string ToString()
    {
        return $"Task Name:{Name} ,Duration:{Duration} ,Difficulty:{Difficulty}";
    }
}