namespace SprintPlanner.Web.Models;

public class SprintViewModel
{
    public List<DeveloperDto> Developers { get; set; } = null!;
    public List<SprintTaskDto> Tasks { get; set; } = null!;
}