using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using UI.Contracts;
using UI.Services;
using System.Security.Claims;

namespace UI.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [CascadingParameter] protected Task<AuthenticationState> AuthenticationState { get; set; } = default!;

        [Inject] NavigationManager NavigationManager { get; set; } = default!;

        [Inject] public IAuthenticationService AuthenticationService { get; set; }

        [Inject] IStateChangeService StateChangeService { get; set; } = default!;


        protected async override Task OnInitializedAsync()
        {
            StateChangeService.RefreshRequested += StateHasChanged;

            var user = (await AuthenticationState).User;
            if (user.Identity.IsAuthenticated)
            {
                var role = user.Claims.Where(x => x.Type.Equals(ClaimTypes.Role, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                //switch (role?.Value)
                //{
                //    //ToDo navigation based on role
                //    default:
                //        NavigationManager.NavigateTo("");
                //        break;
                //}
            }
            else
            {
                NavigationManager.NavigateTo("");
            }
            
        }

        bool _drawerOpen = true;

        private MudTheme _theme = new()
        {
            PaletteLight = new PaletteLight
            {
                Background = "rgba(241, 241, 242, 1)",             // Light gray for the app background
                AppbarBackground = "rgba(26, 22, 73, 1)",          // Dark blue for the header's background
                Primary = "rgba(26, 22, 73, 1)",                // Darker light sky blue for buttons
                PrimaryContrastText = "rgba(255, 255, 255, 1)",    // White for button text, ensuring readability
                Secondary = "rgba(26, 22, 73, 1)",                 // Dark blue for secondary buttons/elements
                Tertiary = "rgba(26, 22, 72, 1)",                  // Similar dark blue for tertiary color
                TextPrimary = "rgba(26, 22, 73, 1)",               // Dark blue for headline text
                TextSecondary = "rgba(26, 22, 73, 0.85)",          // Darker transparent dark blue for paragraph text
                ActionDefault = "rgba(26, 22, 73, 1)",             // Dark blue for illustration stroke
                Surface = "rgba(230, 230, 232, 1)",                // Slightly darker gray for elements like cards/modals
                DrawerBackground = "rgba(230, 230, 232, 1)",       // Matching slightly darker gray for side drawers/panels
                Info = "rgba(90, 140, 180, 1)",                    // Darker sky blue for highlights
                Error = "rgba(255, 69, 0, 1)"                      // Standard error color remains the same
            }
        };
        private bool _isDarkMode;
        void ToggleDarkMode()
        {
            _isDarkMode = !_isDarkMode;
        }

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        protected async void Logout()
        {
            await AuthenticationService.Logout();
        }


    }
}
