using UI.Responses;
using UI.Services.Base;
using UI.ViewModels.Requests;

namespace UI.Contracts
{
    public interface IApiRequestDataService
    {
        Task<ServiceResponse<ApiRequestViewModel>> CreateApiRequest(string name, string curl, CancellationToken cancellationToken);
        Task<ServiceResponse<RequestResult>> TestExecuteApiRequest(string curl, CancellationToken cancellationToken);
        Task<ServiceResponse<RequestResult>> ExecuteApiRequest(Guid requestId, CancellationToken cancellationToken);

        Task<ServiceResponse<List<ApiRequestViewModel>>> GetApiRequestAll(CancellationToken cancellationToken);

        Task<ServiceResponse<ApiRequestViewModel>> GetApiRequestById(Guid requestId, CancellationToken cancellationToken);

        Task<ServiceResponse<ApiRequestViewModel>> UpdateApiRequest(ApiRequestViewModel model, CancellationToken cancellationToken);

    }
}
