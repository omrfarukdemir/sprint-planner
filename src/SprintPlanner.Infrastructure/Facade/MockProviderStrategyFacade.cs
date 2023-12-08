namespace SprintPlanner.Infrastructure.Facade;

public class MockProviderStrategyFacade : IMockProviderStrategyFacade
{
    private readonly IEnumerable<IMockProviderStrategy> _mockProviders;

    public MockProviderStrategyFacade(IEnumerable<IMockProviderStrategy> mockProviders)
    {
        // ReSharper disable once PossibleMultipleEnumeration
        Guard.Against.NullOrEmpty(mockProviders, nameof(mockProviders));

        // ReSharper disable once PossibleMultipleEnumeration
        _mockProviders = mockProviders;
    }

    public async IAsyncEnumerable<TaskResponse?> GetTasksAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var provider in _mockProviders)
        {
            await foreach (var taskResponse in provider.GetTasksAsync(cancellationToken))
            {
                yield return taskResponse;
            }
        }
    }
}