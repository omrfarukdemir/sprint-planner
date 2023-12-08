namespace SprintPlanner.Application.Developers.Queries.GetAllDeveloperWithTasks;

public record GetAllDevelopersWithTasksQuery : IRequest<List<DeveloperWithTasksDto>>;