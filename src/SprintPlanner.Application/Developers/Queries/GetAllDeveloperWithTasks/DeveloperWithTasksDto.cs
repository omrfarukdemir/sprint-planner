namespace SprintPlanner.Application.Developers.Queries.GetAllDeveloperWithTasks;

public record DeveloperWithTasksDto(
    int Id,
    string Name,
    int Duration,
    int Difficulty,
    List<SprintTaskDto> Tasks);