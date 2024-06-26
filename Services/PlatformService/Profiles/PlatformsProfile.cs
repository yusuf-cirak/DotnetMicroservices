using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public sealed class PlatformsProfile : Profile
    {
        
        public PlatformsProfile()
        {
            CreateMap<Platform,GetPlatformDto>();
            CreateMap<CreatePlatformDto,Platform>();
            CreateMap<GetPlatformDto,PlatformPublishedDto>();
            CreateMap<Platform,GrpcPlatformModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
            .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost));
        }
    }
}