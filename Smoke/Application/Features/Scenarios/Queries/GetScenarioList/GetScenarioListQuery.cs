using Application.Abstractions.Messaging;
using Domain.Entities.Scenarios;

namespace Application.Features.Requests.HttpRequest.Queries.GetApiRequestList;

public record GetScenarioListQuery() : IQuery<IEnumerable<Scenario>>;
