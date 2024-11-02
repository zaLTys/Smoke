using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Entities.Scenarios;

namespace Application.Features.Requests.HttpRequest.Queries.GetApiRequest;

internal sealed class GetScenarioQueryHandler : IQueryHandler<GetScenarioQuery, Scenario>
{
    private readonly IScenarioRepository _scenarioRepository;

    public GetScenarioQueryHandler(IScenarioRepository scenarioRepository)
    {
        _scenarioRepository = scenarioRepository;
    }

    public async Task<Scenario> Handle(GetScenarioQuery query, CancellationToken cancellationToken)
    {
        var result = _scenarioRepository.GetById(query.Id);
        if (result == null)
        {
            throw new KeyNotFoundException($"ApiRequest with ID {query.Id} was not found.");
        }

        return result;
    }
}
