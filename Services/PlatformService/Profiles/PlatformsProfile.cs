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
        }
    }
}