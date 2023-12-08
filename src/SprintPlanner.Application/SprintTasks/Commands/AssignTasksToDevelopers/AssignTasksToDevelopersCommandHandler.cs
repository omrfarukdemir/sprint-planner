namespace SprintPlanner.Application.SprintTasks.Commands.AssignTasksToDevelopers;

public class AssignTasksToDevelopersCommandHandler : IRequestHandler<AssignTasksToDevelopersCommand>
{
    private readonly IPlanningAlgorithmFactory _planningAlgorithmFactory;

    public AssignTasksToDevelopersCommandHandler(IPlanningAlgorithmFactory planningAlgorithmFactory)
    {
        Guard.Against.Null(planningAlgorithmFactory, nameof(planningAlgorithmFactory));

        _planningAlgorithmFactory = planningAlgorithmFactory;
    }

    public async Task Handle(
        AssignTasksToDevelopersCommand request, CancellationToken cancellationToken)
    {
        var planningAlgorithm = _planningAlgorithmFactory.Create(request.AlgorithmType);


        await planningAlgorithm?.AssignTasksAsync(cancellationToken)!;
    }
}