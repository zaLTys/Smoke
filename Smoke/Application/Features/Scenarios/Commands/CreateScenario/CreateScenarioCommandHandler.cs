using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Entities.Scenarios;

namespace Application.Features.Requests.HttpRequest.Commands.CreateApiRequest;

internal sealed class CreateScenarioCommandHandler : ICommandHandler<CreateScenarioCommand, Scenario>
{
    private readonly ICurlParserService _curlParserService;
    private readonly IScenarioRepository _scenarioRepository;

    public CreateScenarioCommandHandler(ICurlParserService curlParserService, IScenarioRepository scenarioRepository)
    {
        _curlParserService = curlParserService;
        _scenarioRepository = scenarioRepository;
    }

    public async Task<Scenario> Handle(CreateScenarioCommand command, CancellationToken cancellationToken)
    {
        return _scenarioRepository.Save(Scenario.Default(command.Name));
    }
}