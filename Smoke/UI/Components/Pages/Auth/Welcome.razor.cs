using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using UI.Contracts;

namespace UI.Components.Pages.Auth
{
    public partial class Welcome : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }

        [Inject] IToastService ToastService { get; set; } = default!;

        [Inject] private IAuthenticationService AuthenticationService { get; set; }

        public bool Authenticated = false;

        protected override async Task OnInitializedAsync()
        {
            //if (!Authenticated)
            //{
            //    var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            //if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("Code", out var param))
            //{
            if (await AuthenticationService.AuthenticateDummy("")) //param.First()
            {
                Authenticated = true;
                ToastService.ShowSuccess("Login success");
                NavigationManager.NavigateTo("/");
            }
            else
            {
                ToastService.ShowError("Login failure");
            }
            //}
            // }
        }
    }
}
