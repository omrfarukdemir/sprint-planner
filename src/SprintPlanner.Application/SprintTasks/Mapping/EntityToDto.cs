namespace SprintPlanner.Application.SprintTasks.Mapping;

public class EntityToDto:Profile
{
    public EntityToDto()
    {
        CreateMap<SprintTask, SprintTaskDto>();
    }
}