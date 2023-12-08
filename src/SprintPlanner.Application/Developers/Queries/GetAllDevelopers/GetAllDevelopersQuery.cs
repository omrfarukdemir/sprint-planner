namespace SprintPlanner.Application.Developers.Queries.GetAllDevelopers;

public record GetAllDevelopersQuery : IRequest<List<DeveloperDto>>;