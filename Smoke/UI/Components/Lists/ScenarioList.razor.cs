using Microsoft.AspNetCore.Components;
using UI.Components.Loader;
using UI.Contracts;
using UI.Responses;
using UI.ViewModels.Scenarios;

namespace UI.Components.Lists
{
    public partial class ScenarioList : DataLoader
    {
        [Inject]
        public IScenarioDataService ScenarioDataService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Parameter]
        public EventCallback<ScenarioViewModel> OnSelected { get; set; }

        public List<ScenarioViewModel> Scenarios { get; set; } = new List<ScenarioViewModel>();

        private List<ScenarioViewModel> FilteredScenarios { get; set; } = new List<ScenarioViewModel>();

        private string _searchText = string.Empty;

        private string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    UpdateFilteredScenarios();
                }
            }
        }

        private void UpdateFilteredScenarios()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredScenarios = Scenarios;
            }
            else
            {
                FilteredScenarios = Scenarios.Where(r => r.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            InvokeAsync(StateHasChanged);
        }

        protected override List<Task<IServiceResponse>> DataLoadRequests
        {
            get
            {
                var tasks = new List<Task<IServiceResponse>>();
                var apiRequestsTask = ScenarioDataService.GetAllScenarios(Cts.Token);

                tasks.Add(Task.Run(async () => await apiRequestsTask as IServiceResponse));

                return tasks;
            }
        }

        protected override async Task HandleLoadSuccess(IServiceResponse response)
        {
            if (response is ServiceResponse<List<ScenarioViewModel>> scenariosResponse)
            {
                Scenarios = scenariosResponse.Data;
                UpdateFilteredScenarios();
            }
            else
            {
                throw new InvalidOperationException($"Unexpected response type: {response.GetType().Name}");
            }
        }
    }
}
