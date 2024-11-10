using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using UI.Components.Loader;
using UI.Contracts;
using UI.Responses;
using UI.Services.Base;
using UI.ViewModels.Requests;
using UI.ViewModels.Scenarios;

namespace UI.Components.Pages.Scenarios
{
    public partial class RegisterScenario : DataLoader
    {
        [Inject] IToastService ToastService { get; set; } = default!;
        [Inject] public IApiRequestDataService ApiRequestDataService { get; set; }
        [Inject] public IScenarioDataService ScenarioDataService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        public string ScenarioNameToRegister { get; set; } = string.Empty;
        public List<ApiRequestViewModel> Requests { get; set; } = new List<ApiRequestViewModel>();


        public string Message { get; set; }
        public string Output { get; set; } = string.Empty;
        public ScenarioViewModel RegisteredScenario { get; set; } = new ScenarioViewModel();

        private List<ScenarioStepViewModel> ScenarioSteps { get; set; } = new();


        private async void Register()
        {
            if (ScenarioNameToRegister == null || ScenarioNameToRegister == string.Empty)
            {
                ToastService.ShowError("Please provide request name");
            }
            else
            {
                if (!Requests.Any(x => x.Name.ToUpper() == ScenarioNameToRegister.ToUpper()))
                {
                    var created = await ScenarioDataService.CreateScenario(ScenarioNameToRegister, Cts.Token);
                    if (created.Success)
                    {
                        ToastService.ShowSuccess("Scenario registered");
                        RegisteredScenario = created.Data;
                        StateHasChanged();
                    }
                }
                else
                {
                    ToastService.ShowError("Request with that name already exists");
                }
            }
        }


        private void AddStep(ApiRequestViewModel request)
        {
            var newStep = new ScenarioStepViewModel
            {
                Id = Guid.NewGuid(),
                StepType = Domain.Primitives.StepType.HttpRequest, // Ensure this enum value exists
                RequestId = request.Id,
                RequestName = request.Name,
                Order = ScenarioSteps.Count + 1,
                DependsOn = new List<Guid>(),
                Mappings = new Dictionary<string, string>(),
                TimeOut = null,
                DelayAfter = null
            };

            ScenarioSteps.Add(newStep);
            UpdateSteps();
            StateHasChanged();
        }

        private void RemoveStep(ScenarioStepViewModel step)
        {
            ScenarioSteps.Remove(step);
            UpdateSteps();
        }

        private void UpdateSteps()
        {
            RegisteredScenario.Steps = ScenarioSteps;
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
