namespace SprintPlanner.Web.Controllers;

public class SprintController : Controller
{
    private readonly IMediator _mediator;

    public SprintController(IMediator mediator)
    {
        Guard.Against.Null(mediator, nameof(mediator));

        _mediator = mediator;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var viewModel = new SprintViewModel
        {
            Developers = await _mediator.Send(new GetAllDevelopersQuery(), cancellationToken),
            Tasks = await _mediator.Send(new GetAllSprintTasksQuery(), cancellationToken)
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Schedule(PlanningAlgorithmType algorithmType, CancellationToken cancellationToken)
    {
        await _mediator
            .Send(new AssignTasksToDevelopersCommand(algorithmType), cancellationToken);

        var model = await _mediator
            .Send(new GetAllDevelopersWithTasksQuery(), cancellationToken);

        return View(model);
    }
}