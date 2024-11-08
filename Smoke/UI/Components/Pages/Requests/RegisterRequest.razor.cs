using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Runtime;
using UI.Components.Loader;
using UI.Contracts;
using UI.Responses;
using UI.ViewModels.Requests;

namespace UI.Components.Pages.Requests
{
    public partial class RegisterRequest : DataLoader
    {
        [Inject] IToastService ToastService { get; set; } = default!;
        [Inject] public IApiRequestDataService ApiRequestDataService { get; set; }
        [Inject] private IAuthenticationService AuthenticationService { get; set; }

        public RegisterRequestViewModel RequestToRegister { get; set; } = new RegisterRequestViewModel();
        public List<ApiRequestViewModel> Requests { get; set; } = new List<ApiRequestViewModel>();

        [Inject] public NavigationManager NavigationManager { get; set; }

        public string Message { get; set; }
        public string Output { get; set; } = string.Empty;
        public ApiRequestViewModel RegisteredRequest { get; set; } = null;

        private List<HeaderViewModel> HeaderEntries { get; set; } = new();



        private async void HandleRegister()
        {
            if (RequestToRegister.Name == null || RequestToRegister.Name == string.Empty)
            {
                ToastService.ShowError("Please provide request name");
            }


            if (!Requests.Any(x => x.Name.ToUpper() == RequestToRegister.Name.ToUpper()))
            {
                var created = await ApiRequestDataService.CreateApiRequest(RequestToRegister.Name, RequestToRegister.Curl, Cts.Token);
                if (created.Success)
                {
                    ToastService.ShowSuccess("Request registered");
                    RegisteredRequest = created.Data;
                    HeaderEntries = created.Data.ApiRequestData.Headers;
                    StateHasChanged();
                }
            }
            ToastService.ShowError("Request with that name already exists");

        }

        protected async void Execute()
        {
            var response = await ApiRequestDataService.ExecuteApiRequest(RequestToRegister.Curl, Cts.Token);
            if (response.Data.IsSuccess)
            {
                Output = response.Data.Response;
                StateHasChanged();
            }
            else
            {
                ToastService.ShowError(response.Message);
                ErrorMessage = response.Data.ErrorMessage;
                Output = response.Data.ErrorMessage;
                StateHasChanged();
            }
        }

        protected async void Update()
        {
            var response = await ApiRequestDataService.UpdateApiRequest(RegisteredRequest, Cts.Token);
            if (response.Success)
            {
                Output = JsonConvert.SerializeObject(response.Data, Formatting.Indented);
                StateHasChanged();
            }
            else
            {
                ToastService.ShowError(response.Message);
                ErrorMessage = response.Message!;
            }
        }

        private void AddHeader()
        {
            HeaderEntries.Add(new HeaderViewModel("", ""));
            UpdateHeaders();
        }

        private void RemoveHeader(HeaderViewModel header)
        {
            HeaderEntries.Remove(header);
            UpdateHeaders();
        }

        private void UpdateHeaders()
        {
            RegisteredRequest.ApiRequestData.Headers = HeaderEntries;
        }


        protected override List<Task<IServiceResponse>> DataLoadRequests
        {
            get
            {
                var tasks = new List<Task<IServiceResponse>>();

                //var teamsTask = TeamDataService.GetTeamsWithAthletes(Cts.Token);
                //var athletesTask = AthleteDataService.GetAllAthletes(Cts.Token);

                //tasks.Add(Task.Run(async () => await teamsTask as IServiceResponse));
                //tasks.Add(Task.Run(async () => await athletesTask as IServiceResponse));

                return tasks;
            }

        }


        protected override Task HandleLoadSuccess(IServiceResponse response)
        {
            //switch (response)
            //{
            //    case ServiceResponse<List<TeamManagementListViewModel>> teamResponse when teamResponse.Success:
            //        Teams = teamResponse.Data;
            //        TeamAthletes = teamResponse.Data.SelectMany(x => x.Athletes).ToList();
            //        break;
            //    case ServiceResponse<List<AthleteListViewModel>> athleteResponse when athleteResponse.Success:
            //        var teamlessAthletes = athleteResponse.Data.Where(x => x.TeamId == null).Select(x => new TeamManagementAthleteViewModel
            //        {
            //            AthleteId = x.Id,
            //            Name = $"{x.FirstName} {x.LastName}",
            //            ProfileImageUrl = x.ProfileImageUrl,
            //            TeamId = Guid.Empty
            //        }).ToList();
            //        TeamAthletes.AddRange(teamlessAthletes);
            //        break;
            //    default:
            //        throw new InvalidOperationException($"Unhandled response type: {response.GetType().Name}");
            //}

            return Task.CompletedTask;
        }
    }
}
