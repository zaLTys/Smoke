using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Entities.Scenarios;

namespace Application.Features.Requests.HttpRequest.Commands.CreateApiRequest;

internal sealed class UpdateScenarioCommandHandler : ICommandHandler<UpdateScenarioCommand, Scenario>
{
    private readonly ICurlParserService _curlParserService;
    private readonly IScenarioRepository _scenarioRepository;

    public UpdateScenarioCommandHandler(ICurlParserService curlParserService, IScenarioRepository scenarioRepository)
    {
        _curlParserService = curlParserService;
        _scenarioRepository = scenarioRepository;
    }

    public async Task<Scenario> Handle(UpdateScenarioCommand command, CancellationToken cancellationToken)
    {
        return _scenarioRepository.Update(command.Scenario);
    }
}