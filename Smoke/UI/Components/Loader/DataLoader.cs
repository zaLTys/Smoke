using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using UI.Responses;
using UI.Services;

namespace UI.Components.Loader
{
    public abstract class DataLoader : ComponentBase, IDisposable
    {
        [Inject] IToastService ToastService { get; set; } = default!;
        public LoadingState State { get; set; } = LoadingState.Loading;
        protected string ErrorMessage { get; set; } = string.Empty;

        protected CancellationTokenSource Cts = new CancellationTokenSource();
        [Inject] IStateChangeService StateChangeService { get; set; } = default!;

        protected abstract List<Task<IServiceResponse>> DataLoadRequests { get; }

        protected override async Task OnInitializedAsync()
        {
            StateChangeService.RefreshRequested += async () =>
            {
                await LoadDataAsync(); //reload data
                StateHasChanged(); // Trigger UI re-render
            };

            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var responses = await Task.WhenAll(DataLoadRequests);

                foreach (var response in responses)
                {
                    if (response.Success)
                    {
                        await HandleLoadSuccess(response);
                    }
                    else
                    {
                        ToastService.ShowError(response.Message);
                        ErrorMessage = response.Message!;
                        State = LoadingState.Error;
                        break;
                    }
                }

                if (State != LoadingState.Error)
                {
                    State = LoadingState.Loaded;
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.ToString();
                State = LoadingState.Error;
            }
        }

        protected abstract Task HandleLoadSuccess(IServiceResponse response);

        public virtual void Dispose()
        {
            StateChangeService.RefreshRequested -= StateHasChanged;
            Cts.Cancel();
            Cts.Dispose();
        }
    }
}
