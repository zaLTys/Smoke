using Application.Abstractions.Messaging;
using Application.Features.Requests.HttpRequest.Queries.GetApiRequestList;
using Domain.Abstractions.Repositories;
using Domain.Entities.Scenarios;

namespace Application.Features.Requests.HttpRequest.Queries.GetApiRequest;

internal sealed class GetScenarioListQueryHandler : IQueryHandler<GetScenarioListQuery, IEnumerable<Scenario>>
{
    private readonly IScenarioRepository _scenarioRepository;

    public GetScenarioListQueryHandler(IScenarioRepository scenarioRepository)
    {
        _scenarioRepository = scenarioRepository;
    }

    public async Task<IEnumerable<Scenario>> Handle(GetScenarioListQuery query, CancellationToken cancellationToken)
    {
        var result = _scenarioRepository.GetAll();
        return result;
    }
}
