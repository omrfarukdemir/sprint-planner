namespace SprintPlanner.Application.Developers.Queries.GetAllDevelopers;

public record DeveloperDto(
    int Id,
    string Name,
    int Duration,
    int Difficulty);