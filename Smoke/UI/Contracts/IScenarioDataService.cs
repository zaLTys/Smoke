using UI.Responses;
using UI.ViewModels.Scenarios;

namespace UI.Contracts
{
    public interface IScenarioDataService
    {
        Task<ServiceResponse<bool>> AssignApiRequestToScenario(long ApiRequestId, Guid scenarioId, CancellationToken cancellationToken);
        Task<ServiceResponse<List<ScenarioDetailsViewModel>>> GetAllScenarios(CancellationToken cancellationToken);
        Task<ServiceResponse<List<ScenarioManagementListViewModel>>> GetScenariosWithApiRequests(CancellationToken cancellationToken);
        Task<ServiceResponse<bool>> CreateScenario(string scenarioName, CancellationToken cancellationToken);
        Task<ServiceResponse<bool>> DeleteScenario(Guid scenarioId, CancellationToken cancellationToken);
        Task<ServiceResponse<ScenarioDetailsViewModel>> GetScenario(Guid scenarioId, CancellationToken cancellationToken);
    }
}
