using AutoMapper;
using FluentAssertions;
using MatchDataManager.DataBase.Dto.Location;
using MatchDataManager.Application.Mappers;
using MatchDataManager.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchDataManager.Infrastructure.Mappers;

namespace MatchDataManager.UnitTest.MapperTests
{
    public class LocationMapperTest
    {
        [Fact]
        public void CreateMap_LocationToLocationDto_MapsCorrectly()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<LocationMappingProfile>(); });

            IMapper mapper = configuration.CreateMapper();

            var location = new Location
            {
                Name = "Location 1",
                City = "City 1"
            };

            // Act
            var locationDto = mapper.Map<LocationDto>(location);

            // Assert

            locationDto.Name.Should().Be("Location 1");
            locationDto.City.Should().Be("City 1");
        }

        [Fact]
        public void CreateMap_CreateLocationDtoToLocation_MapsCorrectly()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<LocationMappingProfile>(); });

            IMapper mapper = configuration.CreateMapper();

            var createLocationDto = new CreateLocationDto
            {
                Name = "New Location",
                City = "New City"
            };

            // Act
            var location = mapper.Map<Location>(createLocationDto);

            // Assert
            location.Name.Should().Be("New Location");
            location.City.Should().Be("New City");
        }
    }
}