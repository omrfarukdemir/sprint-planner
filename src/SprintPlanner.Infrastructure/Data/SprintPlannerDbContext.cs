namespace SprintPlanner.Infrastructure.Data;

public class SprintPlannerDbContext : DbContext
{
    public SprintPlannerDbContext(DbContextOptions<SprintPlannerDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Error);
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Developer> Developers { get; init; } = null!;
    public DbSet<SprintTask> Tasks { get; init; } = null!;
}