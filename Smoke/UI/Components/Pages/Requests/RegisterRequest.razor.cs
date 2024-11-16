using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using UI.Components.Loader;
using UI.Contracts;
using UI.Responses;
using UI.Services.Base;
using UI.ViewModels.Requests;
using UI.ViewModels.Scenarios;

namespace UI.Components.Pages.Requests
{
    public partial class RegisterRequest : DataLoader
    {
        [Inject] IToastService ToastService { get; set; } = default!;
        [Inject] IApiRequestDataService ApiRequestDataService { get; set; }

        public RegisterRequestViewModel RequestToRegister { get; set; } = new RegisterRequestViewModel();
        public List<ApiRequestViewModel> Requests { get; set; } = new List<ApiRequestViewModel>();

        [Inject] NavigationManager NavigationManager { get; set; }

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
                        NavigateToRequest(created.Data);
                    }
                }
                else
                {
                    ToastService.ShowError("Request with that name already exists");
                }
            }
        }

        private async void NavigateToRequest(ApiRequestViewModel request)
        {
            NavigationManager.NavigateTo($"/requests/{request.Id}");
        }

        private async void NavigateToScenario(ScenarioViewModel scenario)
        {
            NavigationManager.NavigateTo($"/scenarios/{scenario.Id}");
        }

        protected async void Execute()
        {
            var response = await ApiRequestDataService.TestExecuteApiRequest(RequestToRegister.Curl, Cts.Token);

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


        protected override List<Task<IServiceResponse>> DataLoadRequests
        {
            get
            {
                var tasks = new List<Task<IServiceResponse>>();
                return tasks;
            }

        }


        protected override Task HandleLoadSuccess(IServiceResponse response)
        {
            return Task.CompletedTask;
        }
    }
}
