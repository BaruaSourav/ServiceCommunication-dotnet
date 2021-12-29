using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // This file contains the mapping profile to and from Platform DTOs
            // Source -> Target
            CreateMap<Platform, PlatformReadDto>(); // Reading Scenario Model to ReadDto
            CreateMap<PlatformCreateDto, Platform>(); // Writing Scenario Create DTO to Model
            CreateMap<PlatformReadDto, PlatformPublishDto>(); //PlatformRead DTO to PlatformPublishedDto for publishing to Message Bus
        }
    }
}