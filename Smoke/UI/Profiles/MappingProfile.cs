using AutoMapper;
using UI.Services.Base;
using UI.ViewModels.Requests;
using UI.ViewModels.Scenarios;


namespace UI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map from ApiRequest (Generated Code) to ApiRequestViewModel
            CreateMap<ApiRequest, ApiRequestViewModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.ApiRequestData, opt => opt.MapFrom(src => src.ApiRequestData));

            // Map from ApiRequestData (Generated Code) to ApiRequestDataViewModel
            CreateMap<ApiRequestData, ApiRequestDataViewModel>()
                .ForMember(dest => dest.HttpMethod, opt => opt.MapFrom(src => src.HttpMethod.ToString()))
                .ForMember(dest => dest.Headers, opt => opt.MapFrom(src => src.Headers.Select(kv => new HeaderViewModel(kv.Key, kv.Value)).ToList()));

            // Map KeyValuePair to HeaderViewModel
            CreateMap<KeyValuePair<string, string>, HeaderViewModel>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            // Reverse mappings for ApiRequestViewModel to ApiRequest (Generated Code)
            CreateMap<ApiRequestViewModel, ApiRequest>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<RequestType>(src.Type)))
                .ForMember(dest => dest.ApiRequestData, opt => opt.MapFrom(src => src.ApiRequestData))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())    // Ignore CreatedDate
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());  // Ignore ModifiedDate

            CreateMap<ApiRequestDataViewModel, ApiRequestData>()
                .ForMember(dest => dest.HttpMethod, opt => opt.MapFrom(src => Enum.Parse<HttpMethodType>(src.HttpMethod)))
                .ForMember(dest => dest.Headers, opt => opt.MapFrom(src => src.Headers.ToDictionary(h => h.Key, h => h.Value)));


            // Map from Scenario to ScenarioViewModel
            CreateMap<Scenario, ScenarioViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps));

            // Map from ScenarioViewModel to Scenario
            CreateMap<ScenarioViewModel, Scenario>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());

            // Map from ScenarioStep to ScenarioStepViewModel
            CreateMap<ScenarioStep, ScenarioStepViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StepType, opt => opt.MapFrom(src => src.StepType))
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.RequestId))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.DependsOn, opt => opt.MapFrom(src => src.DependsOn))
                .ForMember(dest => dest.Mappings, opt => opt.MapFrom(src => src.Mappings))
                .ForMember(dest => dest.TimeOut, opt => opt.MapFrom(src => src.TimeOut))
                .ForMember(dest => dest.DelayAfter, opt => opt.MapFrom(src => src.DelayAfter))
                .ForMember(dest => dest.RequestName, opt => opt.Ignore()); // Assuming RequestName comes from elsewhere

            // Map from ScenarioStepViewModel to ScenarioStep
            CreateMap<ScenarioStepViewModel, ScenarioStep>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StepType, opt => opt.MapFrom(src => src.StepType))
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.RequestId))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.DependsOn, opt => opt.MapFrom(src => src.DependsOn))
                .ForMember(dest => dest.Mappings, opt => opt.MapFrom(src => src.Mappings))
                .ForMember(dest => dest.TimeOut, opt => opt.MapFrom(src => src.TimeOut))
                .ForMember(dest => dest.DelayAfter, opt => opt.MapFrom(src => src.DelayAfter));

            // Map from ScenarioExecutionResult to ScenarioExecutionResultViewModel
            CreateMap<ScenarioExecutionResult, ScenarioExecutionResultViewModel>()
                .ForMember(dest => dest.ScenarioId, opt => opt.MapFrom(src => src.ScenarioId))
                .ForMember(dest => dest.StepResults, opt => opt.MapFrom(src => src.StepResults))
                .ForMember(dest => dest.StartTime, opt => opt.Ignore())
                .ForMember(dest => dest.EndTime, opt => opt.Ignore())
                .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.IsSuccess))
                .ForMember(dest => dest.Logs, opt => opt.MapFrom(src => src.Logs));

            // Map from ScenarioExecutionResultViewModel to ScenarioExecutionResult
            CreateMap<ScenarioExecutionResultViewModel, ScenarioExecutionResult>()
                .ForMember(dest => dest.ScenarioId, opt => opt.MapFrom(src => src.ScenarioId))
                .ForMember(dest => dest.StepResults, opt => opt.MapFrom(src => src.StepResults))
                .ForMember(dest => dest.StartTime, opt => opt.Ignore())
                .ForMember(dest => dest.EndTime, opt => opt.Ignore())
                .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.IsSuccess))
                .ForMember(dest => dest.Logs, opt => opt.MapFrom(src => src.Logs));

            // Map from ScenarioStepResult to ScenarioStepResultViewModel
            CreateMap<ScenarioStepResult, ScenarioStepResultViewModel>()
                .ForMember(dest => dest.StepId, opt => opt.MapFrom(src => src.StepId))
                .ForMember(dest => dest.Response, opt => opt.MapFrom(src => src.Response))
                .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.IsSuccess))
                .ForMember(dest => dest.ErrorMessage, opt => opt.MapFrom(src => src.ErrorMessage))
                .ForMember(dest => dest.OutputData, opt => opt.MapFrom(src => src.OutputData));

            // Map from ScenarioStepResultViewModel to ScenarioStepResult
            CreateMap<ScenarioStepResultViewModel, ScenarioStepResult>()
                .ForMember(dest => dest.StepId, opt => opt.MapFrom(src => src.StepId))
                .ForMember(dest => dest.Response, opt => opt.MapFrom(src => src.Response))
                .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.IsSuccess))
                .ForMember(dest => dest.ErrorMessage, opt => opt.MapFrom(src => src.ErrorMessage))
                .ForMember(dest => dest.OutputData, opt => opt.MapFrom(src => src.OutputData));
        }
    }

}
