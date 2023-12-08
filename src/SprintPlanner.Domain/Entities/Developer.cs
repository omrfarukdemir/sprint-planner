namespace SprintPlanner.Domain.Entities;

public class Developer : BaseEntity
{
    public string Name { get; init; } = null!;
    public int Duration { get; init; }
    public int Difficulty { get; init; }

    public List<SprintTask>? Tasks { get; set; }
}