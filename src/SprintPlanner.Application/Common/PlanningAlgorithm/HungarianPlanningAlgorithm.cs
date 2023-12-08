namespace SprintPlanner.Application.Common.PlanningAlgorithm;

public class HungarianPlanningAlgorithm : IPlanningAlgorithm
{
    private readonly IRepository<Developer, int> _developerRepository;
    private readonly IRepository<SprintTask, int> _taskRepository;
    public PlanningAlgorithmType AlgorithmType => PlanningAlgorithmType.Hungarian;

    public HungarianPlanningAlgorithm(IRepository<Developer, int> developerRepository,
        IRepository<SprintTask, int> taskRepository)
    {
        Guard.Against.Null(developerRepository, nameof(developerRepository));
        Guard.Against.Null(taskRepository, nameof(taskRepository));

        _developerRepository = developerRepository;
        _taskRepository = taskRepository;
    }

    public async Task AssignTasksAsync(CancellationToken cancellationToken)
    {
        var developers = await _developerRepository
            .GetAllAsync(cancellationToken);

        var developerIdNullableCount = _taskRepository
            .AsQueryable()
            .Count(t => t.DeveloperId == null);

        var limit = (float)developerIdNullableCount / developers.Count;

        for (var i = 1; i <= Math.Ceiling(limit); i++)
        {
            var tasks = _taskRepository
                .AsQueryable()
                .OrderBy(t => t.Duration)
                .Where(t => t.DeveloperId == null)
                .Take(developers.Count)
                .ToList();

            developers = MatrisDifference(tasks, developers);

            var matris = new List<List<int>>();

            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (var developer in developers)
            {
                var row = tasks.Select(task => ExceptedDuration(task, developer) <= task.Duration
                    ? (int)(ExceptedDuration(task, developer) * 100)
                    : 1000).ToList();
                matris.Add(row);
            }


            var plannerAlgorithm = new Munkres(matris.ConvertToDoubleArray());
            plannerAlgorithm.Minimize();
            var solution = plannerAlgorithm.Solution.Select(Convert.ToInt32).ToList();

            AssignToDevelopers(solution, tasks, developers);

            await _taskRepository.UpdateRange(tasks, cancellationToken);
        }
    }

    private static void AssignToDevelopers(IReadOnlyList<int> solution, IReadOnlyList<SprintTask> tasks,
        IReadOnlyList<Developer> developers)
    {
        for (var key = 0; key < solution.Count; key++)
        {
            tasks[solution[key]].DeveloperId = developers[key].Id;
            tasks[solution[key]].ExpectedDuration = ExceptedDuration(tasks[solution[key]], developers[key]);
        }
    }

    private static double ExceptedDuration(SprintTask task, Developer developer)
    {
        // ReSharper disable once PossibleLossOfFraction
        return task.Difficulty * 100 / developer.Difficulty / 100.0;
    }

    private List<Developer> MatrisDifference(ICollection<SprintTask> tasks, List<Developer> developers)
    {
        var diff = 0;
        if (tasks.Count != developers.Count)
        {
            diff = developers.Count - tasks.Count;
        }

        if (diff > 0)
        {
            var developerIds = _taskRepository
                .AsQueryable()
                .GroupBy(t => t.DeveloperId)
                .OrderByDescending(g => g.Sum(t => t.ExpectedDuration))
                .Take(diff)
                .Select(g => g.Key)
                .ToList();

            developers = developers
                .Where(d => !developerIds.Contains(d.Id))
                .ToList();
        }

        return developers;
    }
}