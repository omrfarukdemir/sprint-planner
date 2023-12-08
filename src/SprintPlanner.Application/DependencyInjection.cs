namespace SprintPlanner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        services.AddScoped<IPlanningAlgorithmFactory, PlanningAlgorithmFactory>();
        services.AddScoped<IPlanningAlgorithm, HungarianPlanningAlgorithm>();

        return services;
    }
}