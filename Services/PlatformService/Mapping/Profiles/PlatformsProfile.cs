using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Mapping.Profiles;

public sealed class PlatformsProfile : Profile
{
    public PlatformsProfile()
    {
        CreateMap<Platform, GetPlatformDto>();
        CreateMap<CreatePlatformDto, Platform>();
    }
}