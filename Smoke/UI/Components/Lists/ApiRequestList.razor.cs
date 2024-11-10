using Microsoft.AspNetCore.Components;
using UI.Components.Loader;
using UI.Contracts;
using UI.Responses;
using UI.ViewModels.Requests;

namespace UI.Components.Lists
{
    public partial class ApiRequestList : DataLoader
    {
        [Inject]
        public IApiRequestDataService ApiRequestDataService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Parameter]
        public EventCallback<ApiRequestViewModel> OnRequestSelected { get; set; }

        public List<ApiRequestViewModel> Requests { get; set; } = new List<ApiRequestViewModel>();

        private List<ApiRequestViewModel> FilteredRequests { get; set; } = new List<ApiRequestViewModel>();

        private string _searchText = string.Empty;

        private string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    UpdateFilteredRequests();
                }
            }
        }

        private void UpdateFilteredRequests()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredRequests = Requests;
            }
            else
            {
                FilteredRequests = Requests.Where(r => r.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            InvokeAsync(StateHasChanged);
        }

        protected override List<Task<IServiceResponse>> DataLoadRequests
        {
            get
            {
                var tasks = new List<Task<IServiceResponse>>();
                var apiRequestsTask = ApiRequestDataService.GetApiRequestAll(Cts.Token);

                tasks.Add(Task.Run(async () => await apiRequestsTask as IServiceResponse));

                return tasks;
            }
        }

        protected override async Task HandleLoadSuccess(IServiceResponse response)
        {
            if (response is ServiceResponse<List<ApiRequestViewModel>> apiRequestsResponse)
            {
                Requests = apiRequestsResponse.Data;
                UpdateFilteredRequests();
            }
            else
            {
                throw new InvalidOperationException($"Unexpected response type: {response.GetType().Name}");
            }
        }
    }
}
