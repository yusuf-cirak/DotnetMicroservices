using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using PlatformService;

namespace CommandsService.Profiles
{
    public sealed class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Platform, PlatformReadDto>();

            CreateMap<PlatformPublishedDto,Platform>()
            .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));

            CreateMap<GrpcPlatformModel,Platform>()
            .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Commands, opt => opt.Ignore());
            
        }
    }
}