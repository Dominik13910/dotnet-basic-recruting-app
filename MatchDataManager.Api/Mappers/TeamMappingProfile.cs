using AutoMapper;
using MatchDataManager.Api.Dto.Location;
using MatchDataManager.Api.Dto.Team;
using MatchDataManager.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace MatchDataManager.Api.Mappers
{
    public class TeamMappingProfile : Profile
    {
        public TeamMappingProfile() 
        {
            CreateMap<Team, TeamDto>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => $"{src.Id}")
                )
             .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.Name}")
                )
             .ForMember(
                dest => dest.CoachName,
                opt => opt.MapFrom(src => $"{src.CoachName}")
                );

            CreateMap<CreateTeamDto, Team>();

        }
    }
}
