﻿using AutoMapper;
using Blazored.LocalStorage;
using UI.Contracts;
using UI.Responses;
using UI.Services.Base;
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

        public async Task<ServiceResponse<ScenarioViewModel>> AssignApiRequestToScenario(Guid apiRequestId, Guid scenarioId, CancellationToken cancellationToken)
        {
            var assignStepRequest = new AssignStepRequest
            {
                ScenarioId = scenarioId,
                RequestId = apiRequestId
            };

            var response = await _client.ApiScenariosAssignStepAsync(assignStepRequest, cancellationToken);
            var mappedElement = _mapper.Map<ScenarioViewModel>(response);
            return ServiceResponse<ScenarioViewModel>.FromData(mappedElement);
        }

        public async Task<ServiceResponse<ScenarioViewModel>> CreateScenario(string scenarioName, CancellationToken cancellationToken)
        {
            var createScenarioRequest = new CreateScenarioRequest
            {
                Name = scenarioName
            };

            var response = await _client.ApiScenariosCreateAsync(createScenarioRequest, cancellationToken);
            var mappedElement = _mapper.Map<ScenarioViewModel>(response);
            return ServiceResponse<ScenarioViewModel>.FromData(mappedElement);
        }

        public async Task<ServiceResponse<ScenarioViewModel>> GetScenario(Guid scenarioId, CancellationToken cancellationToken)
        {
            var response = await _client.ApiScenariosAsync(scenarioId, cancellationToken);
            var mappedElement = _mapper.Map<ScenarioViewModel>(response);
            return ServiceResponse<ScenarioViewModel>.FromData(mappedElement);
        }

        //public async Task<ServiceResponse<List<ScenarioViewModel>>> GetAllScenarios(CancellationToken cancellationToken)
        //{
        //    // Assuming there is a client method to get all scenarios
        //    var response = await _client.apis(cancellationToken);
        //    var mappedList = _mapper.Map<List<ScenarioViewModel>>(response);
        //    return ServiceResponse<List<ScenarioViewModel>>.FromData(mappedList);
        //}

        //public async Task<ServiceResponse<bool>> DeleteScenario(Guid scenarioId, CancellationToken cancellationToken)
        //{
        //    // Assuming there is a client method to delete a scenario
        //    await _client.ApiScenariosDeleteAsync(scenarioId, cancellationToken);
        //    return ServiceResponse<bool>.FromData(true);
        //}
    }

}