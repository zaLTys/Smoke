using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using UI.Contracts;

namespace UI.Components.Pages.Auth
{
    public partial class Login
    {
        [Inject] public NavigationManager NavigationManager { get; set; }

        [Inject] IToastService ToastService { get; set; } = default!;

        [Inject] private IAuthenticationService AuthenticationService { get; set; }


        protected async void HandleValidSubmit()
        {
            try
            {
                var authUrl = "/welcome"; //await AuthenticationService.GetAuthenticationUrl();
                NavigationManager.NavigateTo(authUrl);
            }
            catch (Exception e)
            {
                ToastService.ShowError("Invalid auth");
                throw;
            }

        }
    }
}
