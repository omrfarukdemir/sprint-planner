namespace SprintPlanner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SprintDatabase");

        Guard.Against.Null(connectionString, message: "Connection string 'SprintDatabase' not found.");

        services.AddDbContext<SprintPlannerDbContext>((sp, options) =>
        {
            var relativePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..");

            var fullPath = Path.GetFullPath(relativePath);

            var builder = new SqliteConnectionStringBuilder(connectionString);
            builder.DataSource = Path.Combine(fullPath, "Database", builder.DataSource);

            options.UseSqlite(builder.ToString());
        });

        services.AddScoped<SprintPlannerDbContextInitialiser>();
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        services.AddScoped<IMockProviderStrategy, MockProviderStrategyOne>();
        services.AddScoped<IMockProviderStrategy, MockProviderStrategyTwo>();
        services.AddScoped<IMockProviderStrategy, MockProviderStrategyThree>();
        services.AddScoped<IMockProviderStrategyFacade, MockProviderStrategyFacade>();

        return services;
    }
}