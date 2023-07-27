using AutoMapper;
using MatchDataManager.DataBase.Dto.Location;
using MatchDataManager.DataBase.Dto.Match;
using MatchDataManager.DataBase.Models;

namespace MatchDataManager.Application.Mappers
{
    public class MatchMappingProfile : Profile
    {
        public MatchMappingProfile()
        {
            CreateMap<Match, MatchDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => $"{src.Id}")
                )
                .ForMember(
                    dest => dest.FirstTeam,
                    opt => opt.MapFrom(src => $"{src.FirstTeam}")
                )
                .ForMember(
                    dest => dest.SecoundTeam,
                    opt => opt.MapFrom(src => $"{src.SecoundTeam}")
                )
                .ForMember(
                    dest => dest.Location,
                    opt => opt.MapFrom(src => $"{src.Location}")
                )
                .ForMember(
                    dest => dest.StartData,
                    opt => opt.MapFrom(src => $"{src.StartData}")
                );

            CreateMap<CreateMatchDto, Match>();
        }
    }
}