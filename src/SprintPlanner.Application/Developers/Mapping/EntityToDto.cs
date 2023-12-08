namespace SprintPlanner.Application.Developers.Mapping;

public class EntityToDto : Profile
{
    public EntityToDto()
    {
        CreateMap<Developer, DeveloperDto>();
        CreateMap<Developer, DeveloperWithTasksDto>();
    }
}