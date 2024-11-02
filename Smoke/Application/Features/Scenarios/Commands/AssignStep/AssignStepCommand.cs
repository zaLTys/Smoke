using Application.Abstractions.Messaging;
using Domain.Entities.Scenarios;

namespace Application.Features.Requests.HttpRequest.Commands.CreateApiRequest
{
    public record AssignStepCommand(Guid RequestId, Guid ScenarioId, int Order) : ICommand<Scenario>;
}
