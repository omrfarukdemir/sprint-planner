namespace SprintPlanner.Application.Common.PlanningAlgorithm;

public interface IPlanningAlgorithm
{
    public PlanningAlgorithmType AlgorithmType { get; }
    Task AssignTasksAsync(CancellationToken cancellationToken);
}