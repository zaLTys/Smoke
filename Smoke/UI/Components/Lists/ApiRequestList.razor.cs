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

        public List<ApiRequestViewModel> Requests { get; set; } = new List<ApiRequestViewModel>();



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
            }
            else
            {
                throw new InvalidOperationException($"Unexpected response type: {response.GetType().Name}");
            }
        }

        private void NavigateToRequestDetails(Guid requestId)
        {
            // Navigate to the request details page
            NavigationManager.NavigateTo($"/requests/details/{requestId}");
        }

    }
}
