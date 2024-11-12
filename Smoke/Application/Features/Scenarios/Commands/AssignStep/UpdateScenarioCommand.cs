using Application.Abstractions.Messaging;
using Domain.Entities.Scenarios;

namespace Application.Features.Requests.HttpRequest.Commands.CreateApiRequest
{
    public record UpdateScenarioCommand(Scenario Scenario) : ICommand<Scenario>;
}
