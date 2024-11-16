using Blazored.Toast.Services;
using Domain.Primitives;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using UI.Components.Loader;
using UI.Contracts;
using UI.Responses;
using UI.Services;
using UI.ViewModels.Requests;
using UI.ViewModels.Scenarios;

namespace UI.Components.Pages.Scenarios
{
    public partial class EditScenario : DataLoader
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] IStateChangeService StateChangeService { get; set; } = default!;
        [Inject] IToastService ToastService { get; set; } = default!;

        [Inject] public IApiRequestDataService ApiRequestDataService { get; set; }
        [Inject] public IScenarioDataService ScenarioDataService { get; set; }


        [Parameter]
        public Guid? ScenarioId { get; set; }

        public string Output { get; set; } = string.Empty;
        public ScenarioViewModel Scenario { get; set; } = new ScenarioViewModel();
        private List<ScenarioStepViewModel> ScenarioSteps { get; set; } = new();

        private MudDropContainer<ScenarioStepViewModel> _mudDropContainer;



        private async void Save()
        {
            var saved = await ScenarioDataService.UpdateScenario(Scenario, Cts.Token);
            if (saved.Success)
            {
                ToastService.ShowSuccess("Scenario registered");
                Scenario = saved.Data;
                StateHasChanged();
            }

            else
            {
                ToastService.ShowError(saved.Message ?? "Error occurred");
            }
        }

        private async void NavigateToEdit(ScenarioViewModel scenario)
        {
            NavigationManager.NavigateTo($"/scenarios/{scenario.Id}");
        }

        private void AddStep(ApiRequestViewModel request)
        {
            var newStep = new ScenarioStepViewModel
            {
                Id = Guid.NewGuid(),
                StepType = RequestType.HttpRequest,
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
            RefreshContainer();
        }

        private void RemoveStep(ScenarioStepViewModel step)
        {
            ScenarioSteps.Remove(step);
            UpdateSteps();
            RefreshContainer();
        }

        private void UpdateSteps()
        {
            for (int i = 0; i < ScenarioSteps.Count; i++)
            {
                ScenarioSteps[i].Order = i + 1;
            }
            Scenario.Steps = ScenarioSteps;
        }

        private void OnDrop(MudItemDropInfo<ScenarioStepViewModel> dropItem)
        {
            var item = dropItem.Item;
            var index = dropItem.IndexInZone;

            if (item != null)
            {
                ScenarioSteps.Remove(item);
                ScenarioSteps.Insert(index, item);
                UpdateSteps();
                RefreshContainer();
            }
        }

        private void RefreshContainer()
        {
            StateHasChanged();
            _mudDropContainer.Refresh();
        }

        private string GetIconForStepType(RequestType stepType)
        {
            return stepType switch
            {
                RequestType.HttpRequest => Icons.Material.Filled.Http,
                RequestType.AuthRequest => Icons.Material.Filled.Lock,
                RequestType.DbRequest => Icons.Material.Filled.Storage,
                RequestType.Wait => Icons.Material.Filled.HourglassEmpty,
                _ => Icons.Material.Filled.HelpOutline
            };
        }


        protected override List<Task<IServiceResponse>> DataLoadRequests
        {
            get
            {
                var tasks = new List<Task<IServiceResponse>>();

                if (ScenarioId != null)
                {
                    var scenarioTask = ScenarioDataService.GetScenarioById((Guid)ScenarioId, Cts.Token);
                    tasks.Add(Task.Run(async () => await scenarioTask as IServiceResponse));
                }

                return tasks;
            }

        }


        protected override Task HandleLoadSuccess(IServiceResponse response)
        {
            switch (response)
            {
                case ServiceResponse<ScenarioViewModel> scenarioResponse when scenarioResponse.Success:
                    Scenario = scenarioResponse.Data;
                    ScenarioSteps = scenarioResponse.Data.Steps;
                    break;
                default:
                    throw new InvalidOperationException($"Unhandled response type: {response.GetType().Name}");
            }

            return Task.CompletedTask;
        }
    }
}
