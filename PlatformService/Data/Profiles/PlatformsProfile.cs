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
            CreateMap<Platform, GrpcPlatformModel>() //Platform to GrpcPlatformModel
                .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));
        }
    }
}