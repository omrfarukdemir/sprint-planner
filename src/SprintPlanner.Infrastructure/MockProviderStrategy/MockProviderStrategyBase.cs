namespace SprintPlanner.Infrastructure.MockProviderStrategy;

public abstract class MockProviderStrategyBase : IMockProviderStrategy
{
    private readonly string _url;

    protected MockProviderStrategyBase(string url)
    {
        Guard.Against.NullOrEmpty(url, nameof(url), "Url is null or empty");

        _url = url;
    }

    protected Task<IFlurlResponse> SendRequestAsync(CancellationToken cancellationToken = default)
    {
        return _url.AllowAnyHttpStatus().GetAsync(cancellationToken);
    }

    public abstract IAsyncEnumerable<TaskResponse?> GetTasksAsync(CancellationToken cancellationToken = default);
}