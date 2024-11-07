using UI.Responses;
using UI.Services.Base;
using UI.ViewModels;

namespace UI.Contracts
{
    public interface IApiRequestDataService
    {
        Task<ServiceResponse<List<ApiRequestListViewModel>>> GetAllApiRequests(CancellationToken cancellationToken);
        Task<ServiceResponse<ApiRequestDetailsViewModel>> GetApiRequestById(long id, CancellationToken cancellationToken);
        Task<ApiResponse<long>> UpdateApiRequest(ApiRequestDetailsViewModel eventDetailViewModel);
    }
}
