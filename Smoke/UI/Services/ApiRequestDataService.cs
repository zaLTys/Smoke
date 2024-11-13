using Application.Features.Requests.HttpRequest.Commands.ExecuteApiRequest;
using AutoMapper;
using Blazored.LocalStorage;
using UI.Contracts;
using UI.Responses;
using UI.Services.Base;
using UI.ViewModels.Requests;
using ExecuteApiRequest = UI.Services.Base.ExecuteApiRequest;

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
            var response = await _client.ApiRequestsCreateAsync(name, curl, cancellationToken);
            var mappedElement = _mapper.Map<ApiRequestViewModel>(response);
            return ServiceResponse<ApiRequestViewModel>.FromData(mappedElement);
        }

        public async Task<ServiceResponse<RequestResult>> ExecuteApiRequest(Guid requestId, CancellationToken cancellationToken)
        {
            var response = await _client.ApiRequestsExecuteAsync(new ExecuteApiRequest { RequestId = requestId}, cancellationToken);
            var mappedElement = _mapper.Map<RequestResult>(response);
            return ServiceResponse<RequestResult>.FromData(mappedElement);
        }

        public async Task<ServiceResponse<RequestResult>> TestExecuteApiRequest(string curl, CancellationToken cancellationToken)
        {
            var response = await _client.ApiRequestsTestExecuteAsync(curl, cancellationToken);
            var mappedElement = _mapper.Map<RequestResult>(response);
            return ServiceResponse<RequestResult>.FromData(mappedElement);
        }

        public async Task<ServiceResponse<ApiRequestViewModel>> UpdateApiRequest(ApiRequestViewModel model, CancellationToken cancellationToken)
        {
            var mappedModel = _mapper.Map<ApiRequest>(model);
            var response = await _client.ApiRequestsUpdateAsync(mappedModel, cancellationToken);
            var mappedElement = _mapper.Map<ApiRequestViewModel>(response);
            return ServiceResponse<ApiRequestViewModel>.FromData(mappedElement);
        }

        public async Task<ServiceResponse<List<ApiRequestViewModel>>> GetApiRequestAll(CancellationToken cancellationToken)
        {
            var response = await _client.ApiRequestsAllAsync(cancellationToken);
            var mappedElement = _mapper.Map<List<ApiRequestViewModel>>(response);
            return ServiceResponse<List<ApiRequestViewModel>>.FromData(mappedElement);
        }

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
