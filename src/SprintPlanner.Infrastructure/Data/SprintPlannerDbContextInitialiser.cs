namespace SprintPlanner.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<SprintPlannerDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class SprintPlannerDbContextInitialiser
{
    private readonly ILogger<SprintPlannerDbContextInitialiser> _logger;
    private readonly SprintPlannerDbContext _context;

    public SprintPlannerDbContextInitialiser(
        ILogger<SprintPlannerDbContextInitialiser> logger,
        SprintPlannerDbContext context)
    {
        Guard.Against.Null(logger, nameof(logger));
        Guard.Against.Null(context, nameof(context));

        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (await _context.Developers.AnyAsync())
        {
            return;
        }

        var developers = Enumerable.Range(1, 5)
            .Select(i => new Developer() { Name = $"DEV{i}", Difficulty = i, Duration = i })
            .ToList();

        await _context.Developers.AddRangeAsync(developers);
        await _context.SaveChangesAsync();
    }
}