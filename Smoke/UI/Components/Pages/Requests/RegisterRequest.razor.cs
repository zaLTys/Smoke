﻿using Blazored.Toast.Services;
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

        public RegisterRequestViewModel RequestToRegister { get; set; } = new RegisterRequestViewModel();
        public List<ApiRequestViewModel> Requests { get; set; } = new List<ApiRequestViewModel>();

        [Inject] public NavigationManager NavigationManager { get; set; }

        public string Message { get; set; }
        public string ExecutionResult { get; set; } = string.Empty;

        [Inject] private IAuthenticationService AuthenticationService { get; set; }


        private async Task<bool> HandleRegister()
        {
            if (RequestToRegister.Name == null || RequestToRegister.Name == string.Empty)
            {
                ToastService.ShowError("Please provide request name");
                return false;
            }


            if (!Requests.Any(x => x.Name.ToUpper() == RequestToRegister.Name.ToUpper()))
            {
                var created = await ApiRequestDataService.CreateApiRequest(RequestToRegister.Name, RequestToRegister.Curl, Cts.Token);
                if (created.Success)
                {
                    ToastService.ShowSuccess("Request registered");
                    //CreateTeamName = string.Empty;
                    //await RefreshTeams();
                }
                return created.Success;

            }
            ToastService.ShowError("Request with that name already exists");
            return false;

        }

        protected async void Execute()
        {
            var response = await ApiRequestDataService.ExecuteApiRequest(RequestToRegister.Curl, Cts.Token);
            if (response.Success)
            {
                ExecutionResult = response.Data.Response;
                StateHasChanged();
            }
            else
            {
                ToastService.ShowError(response.Message);
                ErrorMessage = response.Message!;
            }
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
