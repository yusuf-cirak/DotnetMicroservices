using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;

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
            
        }
    }
}