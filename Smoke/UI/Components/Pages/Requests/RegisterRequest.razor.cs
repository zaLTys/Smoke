using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using UI.Components.Loader;
using UI.Contracts;
using UI.Responses;
using UI.Services.Base;
using UI.ViewModels.Requests;

namespace UI.Components.Pages.Requests
{
    public partial class RegisterRequest : DataLoader
    {
        [Inject] IToastService ToastService { get; set; } = default!;
        [Inject] IApiRequestDataService ApiRequestDataService { get; set; }

        public RegisterRequestViewModel RequestToRegister { get; set; } = new RegisterRequestViewModel();
        public List<ApiRequestViewModel> Requests { get; set; } = new List<ApiRequestViewModel>();

        [Inject] NavigationManager NavigationManager { get; set; }

        [Parameter]
        public Guid? RequestId { get; set; }

        public string Message { get; set; }
        public string Output { get; set; } = string.Empty;
        public ApiRequestViewModel ApiRequest { get; set; } = null;

        private List<HeaderViewModel> HeaderEntries { get; set; } = new();



        private async void HandleRegister()
        {
            if (RequestToRegister.Name == null || RequestToRegister.Name == string.Empty)
            {
                ToastService.ShowError("Please provide request name");
            }
            else
            {
                if (!Requests.Any(x => x.Name.ToUpper() == RequestToRegister.Name.ToUpper()))
                {
                    var created = await ApiRequestDataService.CreateApiRequest(RequestToRegister.Name, RequestToRegister.Curl, Cts.Token);
                    if (created.Success)
                    {
                        ToastService.ShowSuccess("Request registered");
                        NavigateToEdit(created.Data);
                    }
                }
                else
                {
                    ToastService.ShowError("Request with that name already exists");
                }
            }
        }

        private async void NavigateToEdit(ApiRequestViewModel request)
        {
            NavigationManager.NavigateTo($"/requests/{request.Id}", forceLoad: true);
        }

        protected async void Execute()
        {
            var response = new ServiceResponse<RequestResult>();
            if (ApiRequest != null)
            {
                response = await ApiRequestDataService.ExecuteApiRequest(ApiRequest.Id, Cts.Token);
            }
            else
            {
                response = await ApiRequestDataService.TestExecuteApiRequest(RequestToRegister.Curl, Cts.Token);
            }

            if (response.Data.IsSuccess)
            {
                ToastService.ShowSuccess("Executed successfully");
                Output = response.Data.Response;
                StateHasChanged();
            }
            else
            {
                ToastService.ShowError(response.Data.ErrorMessage);
                ErrorMessage = response.Data.ErrorMessage;
                Output = response.Data.ErrorMessage;
                StateHasChanged();
            }
        }

        protected async void Update()
        {
            var response = await ApiRequestDataService.UpdateApiRequest(ApiRequest, Cts.Token);
            if (response.Success)
            {
                ToastService.ShowSuccess("Saved successfully");
                ApiRequest = response.Data;
                HeaderEntries = response.Data.ApiRequestData.Headers;
                Output = JsonConvert.SerializeObject(response.Data, Formatting.Indented);
                StateHasChanged();
            }
            else
            {
                ToastService.ShowError(response.Message);
                ErrorMessage = response.Message!;
            }
        }

        private void AddHeader()
        {
            HeaderEntries.Add(new HeaderViewModel("", ""));
            UpdateHeaders();
        }

        private void RemoveHeader(HeaderViewModel header)
        {
            HeaderEntries.Remove(header);
            UpdateHeaders();
        }

        private void UpdateHeaders()
        {
            ApiRequest.ApiRequestData.Headers = HeaderEntries;
        }


        protected override List<Task<IServiceResponse>> DataLoadRequests
        {
            get
            {
                var tasks = new List<Task<IServiceResponse>>();

                if (RequestId != null)
                {
                    var requestTask = ApiRequestDataService.GetApiRequestById((Guid)RequestId, Cts.Token);
                    tasks.Add(Task.Run(async () => await requestTask as IServiceResponse));
                }

                return tasks;
            }

        }


        protected override Task HandleLoadSuccess(IServiceResponse response)
        {
            switch (response)
            {
                case ServiceResponse<ApiRequestViewModel> apiRequestResponse when apiRequestResponse.Success:
                    ApiRequest = apiRequestResponse.Data;
                    HeaderEntries = apiRequestResponse.Data.ApiRequestData.Headers;
                    break;
                default:
                    throw new InvalidOperationException($"Unhandled response type: {response.GetType().Name}");
            }

            return Task.CompletedTask;
        }
    }
}
