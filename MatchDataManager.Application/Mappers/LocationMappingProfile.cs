using AutoMapper;
using MatchDataManager.DataBase.Dto.Location;
using MatchDataManager.DataBase.Models;

namespace MatchDataManager.Infrastructure.Mappers
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
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name}")
                )
                .ForMember(
                    dest => dest.City,
                    opt => opt.MapFrom(src => $"{src.City}")
                );

            CreateMap<CreateLocationDto, Location>();
        }
    }
}