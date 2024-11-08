using AutoMapper;
using UI.Services.Base;
using UI.ViewModels.Requests;


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
                .ForMember(dest => dest.Headers, opt => opt.MapFrom(src => src.Headers.Select(kv => new HeaderViewModel (kv.Key,kv.Value)).ToList()));

            // Map KeyValuePair to HeaderViewModel
            CreateMap<KeyValuePair<string, string>, HeaderViewModel>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            // Reverse mappings for ApiRequestViewModel to ApiRequest (Generated Code)
            CreateMap<ApiRequestViewModel, ApiRequest>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<StepType>(src.Type)))
                .ForMember(dest => dest.ApiRequestData, opt => opt.MapFrom(src => src.ApiRequestData))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())    // Ignore CreatedDate
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());  // Ignore ModifiedDate

            CreateMap<ApiRequestDataViewModel, ApiRequestData>()
                .ForMember(dest => dest.HttpMethod, opt => opt.MapFrom(src => Enum.Parse<HttpMethodType>(src.HttpMethod)))
                .ForMember(dest => dest.Headers, opt => opt.MapFrom(src => src.Headers.ToDictionary(h => h.Key, h => h.Value)));

        }
    }
}
