namespace SprintPlanner.Infrastructure.MockProviderStrategy;

//Strategy Pattern
public interface IMockProviderStrategy
{
    IAsyncEnumerable<TaskResponse?> GetTasksAsync(CancellationToken cancellationToken = default);
}