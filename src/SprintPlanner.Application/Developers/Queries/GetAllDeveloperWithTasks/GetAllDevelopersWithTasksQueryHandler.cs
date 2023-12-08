using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace SprintPlanner.Application.Developers.Queries.GetAllDeveloperWithTasks;

public class
    GetAllDevelopersWithTasksQueryHandler : IRequestHandler<GetAllDevelopersWithTasksQuery, List<DeveloperWithTasksDto>>
{
    private readonly IRepository<Developer, int> _developerRepository;
    private readonly IMapper _mapper;

    public GetAllDevelopersWithTasksQueryHandler(
        IRepository<Developer, int> developerRepository,
        IMapper mapper)
    {
        Guard.Against.Null(developerRepository, nameof(developerRepository));
        Guard.Against.Null(mapper, nameof(mapper));

        _developerRepository = developerRepository;
        _mapper = mapper;
    }

    public async Task<List<DeveloperWithTasksDto>> Handle(
        GetAllDevelopersWithTasksQuery request, CancellationToken cancellationToken)
    {
        return await _developerRepository
            .AsQueryable()
            .AsNoTracking()
            .ProjectTo<DeveloperWithTasksDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}