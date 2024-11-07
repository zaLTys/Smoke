using UI.Responses;

namespace UI.Contracts
{
    public interface IScenarioDataService
    {
        Task<ServiceResponse<bool>> AssignApiRequestToScenario(long ApiRequestId, Guid teamId, CancellationToken cancellationToken);
        //Task<ServiceResponse<List<ScenarioListViewModel>>> GetAllScenarios(CancellationToken cancellationToken);
        //Task<ServiceResponse<List<ScenarioManagementListViewModel>>> GetScenariosWithApiRequests(CancellationToken cancellationToken);
        Task<ServiceResponse<bool>> CreateScenario(string teamName, CancellationToken cancellationToken);
        Task<ServiceResponse<bool>> DeleteScenario(Guid teamId, CancellationToken cancellationToken);
        //Task<ServiceResponse<ScenarioDetailsViewModel>> GetScenario(Guid teamId, CancellationToken cancellationToken);
    }
}
