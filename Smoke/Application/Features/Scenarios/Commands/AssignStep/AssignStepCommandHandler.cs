using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Entities.Scenarios;

namespace Application.Features.Requests.HttpRequest.Commands.CreateApiRequest;

internal sealed class AssignStepCommandHandler : ICommandHandler<AssignStepCommand, Scenario>
{
    private readonly ICurlParserService _curlParserService;
    private readonly IScenarioRepository _scenarioRepository;

    public AssignStepCommandHandler(ICurlParserService curlParserService, IScenarioRepository scenarioRepository)
    {
        _curlParserService = curlParserService;
        _scenarioRepository = scenarioRepository;
    }

    public async Task<Scenario> Handle(AssignStepCommand command, CancellationToken cancellationToken)
    {
        return _scenarioRepository.AssignStep(command.RequestId, command.ScenarioId, command.Order);
    }
}