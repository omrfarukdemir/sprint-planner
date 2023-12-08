namespace SprintPlanner.Infrastructure.MockProviderStrategy;

public class MockProviderStrategyThree : MockProviderStrategyBase
{
    private readonly ILogger<MockProviderStrategyThree> _logger;

    public MockProviderStrategyThree(
        IConfiguration configuration,
        ILogger<MockProviderStrategyThree> logger) : base(configuration["ProvidersUrl:Provider3"]!)
    {
        Guard.Against.Null(logger, nameof(logger));

        _logger = logger;
    }

    public override async IAsyncEnumerable<TaskResponse?> GetTasksAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var flurlResponse = await SendRequestAsync(cancellationToken);

        if (!flurlResponse.ResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogError("The request was not successful (url : {Url} , status code : {Code})",
                flurlResponse.ResponseMessage?.RequestMessage?.RequestUri, flurlResponse.StatusCode);

            await Task.CompletedTask;

            yield break;
        }

        var response = await flurlResponse.GetXmlAsync<MockProviderStrategyThreeResponse>();

        foreach (var task in response?.Tasks.Task!)
        {
            yield return new TaskResponse()
            {
                Name = task.Name,
                Duration = task.Duration,
                Difficulty = task.Difficulty
            };
        }
    }
}