﻿using UI.Responses;
using UI.Services.Base;
using UI.ViewModels.Requests;

namespace UI.Contracts
{
    public interface IApiRequestDataService
    {
        Task<ServiceResponse<ApiRequestViewModel>> CreateApiRequest(string name, string curl, CancellationToken cancellationToken);
        Task<ServiceResponse<RequestResult>> ExecuteApiRequest(string curl, CancellationToken cancellationToken);

        //Task<ServiceResponse<ApiRequestViewModel>> GetApiRequestById(Guid id, CancellationToken cancellationToken);
        //Task<ApiResponse<ApiRequestViewModel>> UpdateApiRequest(ApiRequestViewModel eventDetailViewModel);
        Task<ServiceResponse<ApiRequestViewModel>> UpdateApiRequest(ApiRequestViewModel model, CancellationToken cancellationToken);
    }
}
