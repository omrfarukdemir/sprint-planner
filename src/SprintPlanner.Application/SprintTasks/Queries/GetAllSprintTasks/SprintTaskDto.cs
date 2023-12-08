namespace SprintPlanner.Application.SprintTasks.Queries.GetAllSprintTasks;

public record SprintTaskDto(
    int Id,
    string Name,
    int Duration,
    double ExpectedDuration,
    int Difficulty);