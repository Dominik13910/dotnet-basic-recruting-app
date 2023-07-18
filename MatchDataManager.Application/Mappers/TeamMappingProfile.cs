using AutoMapper;
using MatchDataManager.DataBase.Dto.Location;
using MatchDataManager.DataBase.Dto.Team;
using MatchDataManager.DataBase.Models;


namespace MatchDataManager.Application.Mappers
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