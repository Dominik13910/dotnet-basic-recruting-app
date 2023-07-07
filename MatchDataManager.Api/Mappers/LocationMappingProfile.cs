using AutoMapper;
using MatchDataManager.Api.Dto.Location;
using MatchDataManager.Api.Dto.Team;
using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Mappers
{
    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<Location, LocationDto>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => $"{src.Id}")
                )
             .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => $"{src.Name}")
                )
             .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => $"{src.City}")
                );

            CreateMap<CreateTeamDto, TeamDto>();

        }
    }
}
