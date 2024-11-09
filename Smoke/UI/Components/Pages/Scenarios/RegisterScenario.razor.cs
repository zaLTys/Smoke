using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using UI.Components.Loader;
using UI.Contracts;
using UI.Responses;
using UI.ViewModels.Requests;
using UI.ViewModels.Scenarios;

namespace UI.Components.Pages.Scenarios
{
    public partial class RegisterScenario : DataLoader
    {
        [Inject] IToastService ToastService { get; set; } = default!;
        [Inject] public IApiRequestDataService ApiRequestDataService { get; set; }
        [Inject] public IScenarioDataService ScenarioDataService { get; set; }

        public string ScenarioNameToRegister { get; set; } = string.Empty;
        public List<ApiRequestViewModel> Requests { get; set; } = new List<ApiRequestViewModel>();

        [Inject] public NavigationManager NavigationManager { get; set; }

        public string Message { get; set; }
        public string Output { get; set; } = string.Empty;
        public ScenarioViewModel RegisteredScenario { get; set; } = null;

        private List<ScenarioStepViewModel> ScenarioSteps { get; set; } = new();



        private async void Register()
        {
            if (ScenarioNameToRegister == null || ScenarioNameToRegister == string.Empty)
            {
                ToastService.ShowError("Please provide request name");
            }
            else
            {
                if (!Requests.Any(x => x.Name.ToUpper() == ScenarioNameToRegister.ToUpper()))
                {
                    var created = await ScenarioDataService.CreateScenario(ScenarioNameToRegister, Cts.Token);
                    if (created.Success)
                    {
                        ToastService.ShowSuccess("Scenario registered");
                        RegisteredScenario = created.Data;
                        StateHasChanged();
                    }
                }
                else
                {
                    ToastService.ShowError("Request with that name already exists");
                }
            }
        }

        //protected async void Execute()
        //{
        //    var response = await ApiRequestDataService.ExecuteApiRequest(RequestToRegister.Curl, Cts.Token);
        //    if (response.Data.IsSuccess)
        //    {
        //        ToastService.ShowSuccess("Executed successfully");
        //        Output = response.Data.Response;
        //        StateHasChanged();
        //    }
        //    else
        //    {
        //        ToastService.ShowError(response.Data.ErrorMessage);
        //        ErrorMessage = response.Data.ErrorMessage;
        //        Output = response.Data.ErrorMessage;
        //        StateHasChanged();
        //    }
        //}

        //protected async void Update()
        //{
        //    var response = await ApiRequestDataService.UpdateApiRequest(RegisteredScenario, Cts.Token);
        //    if (response.Success)
        //    {
        //        ToastService.ShowSuccess("Saved successfully");
        //        RegisteredScenario = response.Data;
        //        ScenarioSteps = response.Data.ApiRequestData.Headers;
        //        Output = JsonConvert.SerializeObject(response.Data, Formatting.Indented);
        //        StateHasChanged();
        //    }
        //    else
        //    {
        //        ToastService.ShowError(response.Message);
        //        ErrorMessage = response.Message!;
        //    }
        //}

        private void RemoveStep(ScenarioStepViewModel step)
        {
            ScenarioSteps.Remove(step);
            UpdateSteps();
        }

        private void UpdateSteps()
        {
            RegisteredScenario.Steps = ScenarioSteps;
        }


        protected override List<Task<IServiceResponse>> DataLoadRequests
        {
            get
            {
                var tasks = new List<Task<IServiceResponse>>();

                //var scenariosTask = ScenarioDataService.GetScenariosWithApiRequests(Cts.Token);
                //var athletesTask = ApiRequestDataService.GetAllApiRequests(Cts.Token);

                //tasks.Add(Task.Run(async () => await scenariosTask as IServiceResponse));
                //tasks.Add(Task.Run(async () => await athletesTask as IServiceResponse));

                return tasks;
            }

        }


        protected override Task HandleLoadSuccess(IServiceResponse response)
        {
            //switch (response)
            //{
            //    case ServiceResponse<List<ScenarioManagementListViewModel>> scenarioResponse when scenarioResponse.Success:
            //        Scenarios = scenarioResponse.Data;
            //        ScenarioApiRequests = scenarioResponse.Data.SelectMany(x => x.ApiRequests).ToList();
            //        break;
            //    case ServiceResponse<List<ApiRequestListViewModel>> athleteResponse when athleteResponse.Success:
            //        var scenariolessApiRequests = athleteResponse.Data.Where(x => x.ScenarioId == null).Select(x => new ScenarioManagementApiRequestViewModel
            //        {
            //            ApiRequestId = x.Id,
            //            Name = $"{x.FirstName} {x.LastName}",
            //            ProfileImageUrl = x.ProfileImageUrl,
            //            ScenarioId = Guid.Empty
            //        }).ToList();
            //        ScenarioApiRequests.AddRange(scenariolessApiRequests);
            //        break;
            //    default:
            //        throw new InvalidOperationException($"Unhandled response type: {response.GetType().Name}");
            //}

            return Task.CompletedTask;
        }
    }
}
