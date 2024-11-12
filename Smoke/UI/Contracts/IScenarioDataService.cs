using UI.Responses;
using UI.ViewModels.Scenarios;

namespace UI.Contracts
{
    public interface IScenarioDataService
    {
        Task<ServiceResponse<ScenarioViewModel>> GetScenario(Guid scenarioId, CancellationToken cancellationToken);
        Task<ServiceResponse<ScenarioViewModel>> CreateScenario(string scenarioName, CancellationToken cancellationToken);
        Task<ServiceResponse<ScenarioViewModel>> UpdateScenario(ScenarioViewModel scenario, CancellationToken cancellationToken);


        //Task<ServiceResponse<bool>> DeleteScenario(Guid scenarioId, CancellationToken cancellationToken);
        Task<ServiceResponse<List<ScenarioViewModel>>> GetAllScenarios(CancellationToken cancellationToken);
    }
}
