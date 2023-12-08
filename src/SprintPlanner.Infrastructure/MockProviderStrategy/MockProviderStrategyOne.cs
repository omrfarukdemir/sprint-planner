namespace SprintPlanner.Infrastructure.MockProviderStrategy;

public class MockProviderStrategyOne : MockProviderStrategyBase
{
    private readonly ILogger<MockProviderStrategyOne> _logger;

    public MockProviderStrategyOne(
        IConfiguration configuration,
        ILogger<MockProviderStrategyOne> logger) : base(configuration["ProvidersUrl:Provider1"]!)
    {
        Guard.Against.Null(logger, nameof(logger));

        _logger = logger;
    }

    public override async IAsyncEnumerable<TaskResponse> GetTasksAsync(
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

        var response = await flurlResponse.GetJsonAsync<MockProviderStrategyOneResponse>();


        foreach (var task in response.Tasks)
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