using Domain.Entities.Requests;
using Domain.Entities.Scenarios;

namespace Domain.Abstractions.Repositories
{
    public interface IScenarioRepository : IRepository<Scenario>
    {
        Scenario AssignStep(Guid requestId, Guid scenarioId, int order);
    }
}