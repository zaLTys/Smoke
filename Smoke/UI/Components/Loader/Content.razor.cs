using Microsoft.AspNetCore.Components;

namespace UI.Components.Loader
{
    public partial class Content
    {
        [Inject] NavigationManager NavigationManager { get; set; } = default!;

        [Parameter] public bool HasState { get; set; } = true;
        [Parameter] public LoadingState State { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; } = default!;

        [Parameter] public string ErrorMessage { get; set; } = default!;

        [Parameter] public string OnErrorRedirectTo { get; set; } = default!;

        private void RedirectTo()
        {
            NavigationManager.NavigateTo(OnErrorRedirectTo);
        }
    }
}
