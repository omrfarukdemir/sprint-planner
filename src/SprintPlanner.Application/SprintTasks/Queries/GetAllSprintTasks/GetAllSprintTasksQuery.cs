namespace SprintPlanner.Application.SprintTasks.Queries.GetAllSprintTasks;

public record GetAllSprintTasksQuery : IRequest<List<SprintTaskDto>>;