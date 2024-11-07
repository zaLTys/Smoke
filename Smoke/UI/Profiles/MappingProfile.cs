using AutoMapper;
using UI.Services;
using UI.ViewModels;

namespace UI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<ActivityVM, ActivityViewModel>()
            //    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.DateTime));

            //CreateMap<ApiRequestListVM, ApiRequestListViewModel>().ReverseMap();
            //CreateMap<ApiRequestDetailsVM, ApiRequestDetailsViewModel>()
            //    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.DateTime))
            //    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.DateTime));

            //CreateMap<ScenarioApiRequestListVM, ApiRequestListViewModel>();


            //CreateMap<ScenarioListVM, ScenarioListViewModel>();
            //CreateMap<ScenarioDetailsVM, ScenarioDetailsViewModel>();

            //CreateMap<ScenarioManagementApiRequestVm, ScenarioManagementApiRequestViewModel>();
            //CreateMap<ScenarioManagementListVM, ScenarioManagementListViewModel>();

            //CreateMap<AuthenticationResponse, AuthenticationResponseViewModel>().ReverseMap();

        }
    }
}
