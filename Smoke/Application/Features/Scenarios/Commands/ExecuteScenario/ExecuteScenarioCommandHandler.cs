using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Domain.Abstractions.Repositories;
using Domain.Entities.Scenarios;
using Domain.Exceptions;

namespace Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest;

internal sealed class ExecuteScenarioCommandHandler : ICommandHandler<ExecuteScenarioCommand, ExecutionResult>
{
    private readonly IScenarioRepository _scenarioRepository;
    private readonly IExecutionService _executionService;

    public ExecuteScenarioCommandHandler(IScenarioRepository scenarioRepository, IExecutionService executionService)
    {
        _scenarioRepository = scenarioRepository;
        _executionService = executionService;
    }


    public async Task<ExecutionResult> Handle(ExecuteScenarioCommand command, CancellationToken cancellationToken)
    {
        var scenario = _scenarioRepository.GetById(command.ScenarioId);
        if (scenario == null)
        {
            throw new ScenarioNotFoundException(command.ScenarioId);
        }
        return await _executionService.ExecuteScenarioAsync(scenario);
    }
}