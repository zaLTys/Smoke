using Application.Abstractions.Messaging;
using Domain.Entities.Scenarios;

namespace Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest
{
    public record ExecuteScenarioCommand(Guid ScenarioId) : ICommand<ScenarioExecutionResult>;

}
