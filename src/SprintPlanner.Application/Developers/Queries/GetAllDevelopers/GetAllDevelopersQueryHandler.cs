namespace SprintPlanner.Application.Developers.Queries.GetAllDevelopers;

public class GetAllDevelopersQueryHandler : IRequestHandler<GetAllDevelopersQuery, List<DeveloperDto>>
{
    private readonly IRepository<Developer, int> _developerRepository;
    private readonly IMapper _mapper;

    public GetAllDevelopersQueryHandler(
        IRepository<Developer, int> developerRepository,
        IMapper mapper)
    {
        Guard.Against.Null(developerRepository, nameof(developerRepository));
        Guard.Against.Null(mapper, nameof(mapper));

        _developerRepository = developerRepository;
        _mapper = mapper;
    }

    public async Task<List<DeveloperDto>> Handle(
        GetAllDevelopersQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<List<DeveloperDto>>(await _developerRepository.GetAllAsync(cancellationToken));
    }
}