using UI.Responses;
using UI.ViewModels.Scenarios;

namespace UI.Contracts
{
    public interface IScenarioDataService
    {
        Task<ServiceResponse<ScenarioViewModel>> CreateScenario(string scenarioName, CancellationToken cancellationToken);
        Task<ServiceResponse<ScenarioViewModel>> UpdateScenario(ScenarioViewModel scenario, CancellationToken cancellationToken);
        Task<ServiceResponse<ScenarioExecutionResultViewModel>> ExecuteScenario(Guid scenarioId, CancellationToken cancellationToken);
        Task<ServiceResponse<ScenarioViewModel>> GetScenarioById(Guid scenarioId, CancellationToken cancellationToken);
        Task<ServiceResponse<List<ScenarioViewModel>>> GetAllScenarios(CancellationToken cancellationToken);
    }
}
