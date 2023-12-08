namespace SprintPlanner.Domain.Entities;

public class SprintTask : BaseEntity
{
    public string Name { get; init; } = null!;
    public int Duration { get; init; }
    public double ExpectedDuration { get; set; }
    public int Difficulty { get; init; }
    public int? DeveloperId { get; set; }
    public Developer? Developer { get; set; }
}