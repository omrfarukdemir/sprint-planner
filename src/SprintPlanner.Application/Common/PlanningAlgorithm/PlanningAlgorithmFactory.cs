namespace SprintPlanner.Application.Common.PlanningAlgorithm;

public class PlanningAlgorithmFactory : IPlanningAlgorithmFactory
{
    private readonly IEnumerable<IPlanningAlgorithm> _planningAlgorithms;

    public PlanningAlgorithmFactory(IEnumerable<IPlanningAlgorithm> planningAlgorithms)
    {
        // ReSharper disable once PossibleMultipleEnumeration
        Guard.Against.NullOrEmpty(planningAlgorithms, nameof(planningAlgorithms));

        // ReSharper disable once PossibleMultipleEnumeration
        _planningAlgorithms = planningAlgorithms;
    }

    public IPlanningAlgorithm? Create(PlanningAlgorithmType algorithmType)
    {
        var planningAlgorithm = _planningAlgorithms
            .FirstOrDefault(x => x.AlgorithmType == algorithmType);

        if (planningAlgorithm is null)
        {
            throw new NotImplementedException($"Not found that implements {algorithmType.ToString()} algorithm");
        }

        return planningAlgorithm;
    }
}