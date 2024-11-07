using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using UI.Auth;
using UI.Contracts;

namespace UI.Components.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] private IAuthenticationService AuthenticationService { get; set; }
        [Inject] IToastService ToastService { get; set; } = default!;


        protected async override Task OnInitializedAsync()
        {
            await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).GetAuthenticationStateAsync();
        }

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
