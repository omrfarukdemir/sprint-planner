namespace SprintPlanner.Application.Common.PlanningAlgorithm;

public interface IPlanningAlgorithmFactory
{
    IPlanningAlgorithm? Create(PlanningAlgorithmType algorithmType);
}