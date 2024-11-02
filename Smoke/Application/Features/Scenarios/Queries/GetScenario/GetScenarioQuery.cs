using Application.Abstractions.Messaging;
using Domain.Entities.Scenarios;


namespace Application.Features.Requests.HttpRequest.Queries.GetApiRequest;

public record GetScenarioQuery(Guid Id) : IQuery<Scenario>;
