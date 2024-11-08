using AutoMapper;
using Blazored.LocalStorage;
using UI.Contracts;
using UI.Responses;
using UI.Services.Base;
using UI.ViewModels.Requests;
using IClient = UI.Services.Base.IClient;

namespace UI.Services
{
    public class ApiRequestDataService : BaseDataService, IApiRequestDataService
    {

        private readonly IMapper _mapper;

        public ApiRequestDataService(IClient client, IMapper mapper, ILocalStorageService localStorage) : base(client, localStorage)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ApiRequestViewModel>> CreateApiRequest(string name, string curl, CancellationToken cancellationToken)
        {
            var apiRequest = await _client.ApiRequestsCreateAsync(name, curl, cancellationToken);
            var mappedElement = _mapper.Map<ApiRequestViewModel>(apiRequest);
            return ServiceResponse<ApiRequestViewModel>.FromData(mappedElement);
        }

        public async Task<ServiceResponse<RequestResult>> ExecuteApiRequest(string curl, CancellationToken cancellationToken)
        {
            var apiRequest = await _client.ApiRequestsExecuteAsync(curl, cancellationToken);
            var mappedElement = _mapper.Map<RequestResult>(apiRequest);
            return ServiceResponse<RequestResult>.FromData(mappedElement);
        }

        public async Task<ServiceResponse<ApiRequestViewModel>> UpdateApiRequest(ApiRequestViewModel model, CancellationToken cancellationToken)
        {
            var mappedModel = _mapper.Map<ApiRequest>(model);
            var apiRequest = await _client.ApiRequestsUpdateAsync(mappedModel, cancellationToken);
            var mappedElement = _mapper.Map<ApiRequestViewModel>(apiRequest);
            return ServiceResponse<ApiRequestViewModel>.FromData(mappedElement);
        }

        //public async Task<ServiceResponse<ApiRequestViewModel>> GetApiRequestById(Guid id, CancellationToken cancellationToken)
        //{
        //    var apiRequest = await _client.ApiRequestsAsync(id, cancellationToken);
        //    var mappedElement = _mapper.Map<ApiRequestViewModel>(apiRequest);
        //    return ServiceResponse<ApiRequestViewModel>.FromData(mappedElement);
        //}

        //public Task<ApiResponse<ApiRequestViewModel>> UpdateApiRequest(ApiRequestViewModel eventDetailViewModel)
        //{
        //    try
        //    {
        //        //UpdateElementCommand updateElementCommand = _mapper.Map<UpdateElementCommand>(elementDetailViewModel);
        //        //await _client.UpdateElementAsync(updateElementCommand);
        //        return Task.FromResult(new ApiResponse<ApiRequestViewModel>());
        //    }
        //    catch (ApiException ex)
        //    {
        //        throw;
        //        //return Task.FromResult(ConvertApiExceptions<ApiRequest>(ex));
        //    }
        //}
    }
}
