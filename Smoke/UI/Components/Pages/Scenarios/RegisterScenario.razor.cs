using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using UI.Components.Loader;
using UI.Contracts;
using UI.Responses;
using UI.Services;
using UI.ViewModels.Scenarios;

namespace UI.Components.Pages.Scenarios
{
    public partial class RegisterScenario : DataLoader
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] IStateChangeService StateChangeService { get; set; } = default!;
        [Inject] IToastService ToastService { get; set; } = default!;

        [Inject] public IApiRequestDataService ApiRequestDataService { get; set; }
        [Inject] public IScenarioDataService ScenarioDataService { get; set; }


        public string ScenarioNameToRegister { get; set; } = string.Empty;

        private MudDropContainer<ScenarioStepViewModel> _mudDropContainer;
        private List<ScenarioStepViewModel> ScenarioSteps { get; set; } = new();


        private async void Register()
        {
            if (ScenarioNameToRegister == null || ScenarioNameToRegister == string.Empty)
            {
                ToastService.ShowError("Please provide request name");
            }
            else
            {
                var created = await ScenarioDataService.CreateScenario(ScenarioNameToRegister, Cts.Token);
                if (created.Success)
                {
                    ToastService.ShowSuccess("Scenario registered");
                    NavigateToEdit(created.Data);
                }
                else
                {
                    ToastService.ShowError(created.Message ?? "Error occurred");
                }
            }
        }

        private async void NavigateToEdit(ScenarioViewModel scenario)
        {
            NavigationManager.NavigateTo($"/scenarios/{scenario.Id}");
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
