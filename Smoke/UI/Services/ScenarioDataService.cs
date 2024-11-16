using AutoMapper;
using Blazored.LocalStorage;
using UI.Contracts;
using UI.Responses;
using UI.Services.Base;
using UI.ViewModels.Requests;
using UI.ViewModels.Scenarios;

namespace UI.Services
{
    public class ScenarioDataService : BaseDataService, IScenarioDataService
    {
        private readonly IMapper _mapper;

        public ScenarioDataService(IClient client, IMapper mapper, ILocalStorageService localStorage)
            : base(client, localStorage)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ScenarioViewModel>> CreateScenario(string scenarioName, CancellationToken cancellationToken)
        {
            var createScenarioRequest = new Base.CreateScenarioRequest
            {
                Name = scenarioName
            };

            var response = await _client.ApiScenariosCreateAsync(createScenarioRequest, cancellationToken);
            var mappedElement = _mapper.Map<ScenarioViewModel>(response);
            return ServiceResponse<ScenarioViewModel>.FromData(mappedElement);
        }

        public async Task<ServiceResponse<ScenarioViewModel>> UpdateScenario(ScenarioViewModel scenario, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<Scenario>(scenario);
            var updateScenarioRequest = new Base.UpdateScenarioRequest
            {
                Scenario = mapped
            };

            var response = await _client.ApiScenariosUpdateAsync(updateScenarioRequest, cancellationToken);
            var mappedElement = _mapper.Map<ScenarioViewModel>(response);
            return ServiceResponse<ScenarioViewModel>.FromData(mappedElement);
        }

        public async Task<ServiceResponse<ScenarioViewModel>> GetScenario(Guid scenarioId, CancellationToken cancellationToken)
        {
            var response = await _client.ApiScenariosAsync(scenarioId, cancellationToken);
            var mappedElement = _mapper.Map<ScenarioViewModel>(response);
            return ServiceResponse<ScenarioViewModel>.FromData(mappedElement);
        }

        public async Task<ServiceResponse<List<ScenarioViewModel>>> GetAllScenarios(CancellationToken cancellationToken)
        {
            // Assuming there is a client method to get all scenarios
            var response = await _client.ApiScenariosAllAsync(cancellationToken);
            var mappedList = _mapper.Map<List<ScenarioViewModel>>(response);
            return ServiceResponse<List<ScenarioViewModel>>.FromData(mappedList);
        }

        public async Task<ServiceResponse<ScenarioViewModel>> GetScenarioById(Guid scenarioId, CancellationToken cancellationToken)
        {
            var response = await _client.ApiScenariosAsync(scenarioId, cancellationToken);
            var mappedElement = _mapper.Map<ScenarioViewModel>(response);
            return ServiceResponse<ScenarioViewModel>.FromData(mappedElement);
        }
    }

}
