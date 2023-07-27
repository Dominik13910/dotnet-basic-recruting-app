using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MatchDataManager.DataBase.Dto.Location;
using MatchDataManager.DataBase.Dto.Team;
using MatchDataManager.Application.Mappers;
using MatchDataManager.DataBase.Models;

namespace MatchDataManager.UnitTest.MapperTests
{
    public class TeamMapperTest
    {
        [Fact]
        public void CreateMap_LocationToLocationDto_MapsCorrectly()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<TeamMappingProfile>(); });

            IMapper mapper = configuration.CreateMapper();

            var location = new Team
            {
                Name = "Team 1",
                CoachName = "Andrzej"
            };

            // Act
            var locationDto = mapper.Map<TeamDto>(location);

            // Assert

            locationDto.Name.Should().Be("Team 1");
            locationDto.CoachName.Should().Be("Andrzej");
        }

        [Fact]
        public void CreateMap_CreateLocationDtoToLocation_MapsCorrectly()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<TeamMappingProfile>(); });

            IMapper mapper = configuration.CreateMapper();

            var createLocationDto = new CreateTeamDto
            {
                Name = "New Team",
                CoachName = "Andrzej"
            };

            // Act
            var location = mapper.Map<Team>(createLocationDto);

            // Assert
            location.Name.Should().Be("New Team");
            location.CoachName.Should().Be("Andrzej");
        }
    }
}