namespace SprintPlanner.Application.SprintTasks.Queries.GetAllSprintTasks;

public class GetAllSprintTasksQueryHandler : IRequestHandler<GetAllSprintTasksQuery, List<SprintTaskDto>>
{
    private readonly IRepository<SprintTask, int> _sprintTaskRepository;
    private readonly IMapper _mapper;

    public GetAllSprintTasksQueryHandler(
        IRepository<SprintTask, int> sprintTaskRepository,
        IMapper mapper)
    {
        Guard.Against.Null(sprintTaskRepository, nameof(sprintTaskRepository));
        Guard.Against.Null(mapper, nameof(mapper));

        _sprintTaskRepository = sprintTaskRepository;
        _mapper = mapper;
    }

    public async Task<List<SprintTaskDto>> Handle(
        GetAllSprintTasksQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<List<SprintTaskDto>>(await _sprintTaskRepository.GetAllAsync(cancellationToken));
    }
}