using SprintPlanner.Infrastructure.Data;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();


var serviceProvider = new ServiceCollection()
    .AddSingleton(configuration)
    .AddInfrastructureServices(configuration)
    .AddLogging(c => { c.AddConsole(); })
    .BuildServiceProvider();

var sprintPlannerDatabaseInitialiser = serviceProvider.GetRequiredService<SprintPlannerDbContextInitialiser>();
await sprintPlannerDatabaseInitialiser.InitialiseAsync();

var sprintTaskRepository = serviceProvider.GetRequiredService<IRepository<SprintTask, int>>();

if (await sprintTaskRepository.AnyAsync(CancellationToken.None))
{
    Console.WriteLine("Data for tasks available");
    return;
}

var mockProviderStrategyFacade = serviceProvider.GetRequiredService<IMockProviderStrategyFacade>();

var cancellationTokenSource = new CancellationTokenSource();

//Ctrl+C or Ctrl+Break
Console.CancelKeyPress += (_, _) =>
{
    cancellationTokenSource.Cancel();
    Console.WriteLine($"Adding a task has been canceled");
};

await foreach (var task in mockProviderStrategyFacade.GetTasksAsync(cancellationTokenSource.Token))
{
    await sprintTaskRepository.AddAsync(new SprintTask()
    {
        Name = task?.Name!,
        Duration = task!.Duration,
        Difficulty = task.Difficulty
    }, cancellationTokenSource.Token);

    Console.WriteLine($"{task} record added");
}