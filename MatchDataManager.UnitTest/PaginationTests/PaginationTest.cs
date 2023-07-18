using FluentAssertions;
using MatchDataManager.DataBase.Models;
using MatchDataManager.DataBase.Models.Paination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MatchDataManager.DataBase.Dto.Team;
using MatchDataManager.Infrastructure.Services;
using Moq;
using MatchDataManager.Application.Authentication;
using Microsoft.Extensions.Logging;
using EntityFrameworkCoreMock;
using Microsoft.AspNetCore.SignalR;
using MatchDataManager.Application.Mappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using MatchDataManager.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using System.Data.Entity.Infrastructure;
using MockQueryable.Moq;
using MockQueryable.EntityFrameworkCore;
using System.Net.Sockets;

namespace MatchDataManager.UnitTest.Pagination
{
    public class TeamRepositoryPaginationTests
    {
        [Fact]
        public async Task GetAll_CorrectlyAppliesPagination()
        {
            var data = new List<Team>
            {
                new Team { Name = "Team1" },
                new Team { Name = "Team2" },
                new Team { Name = "Team3" },
                new Team { Name = "Team4" },
                new Team { Name = "Team5" },
                new Team { Name = "Team1" },
                new Team { Name = "Team2" },
                new Team { Name = "Team3" },
                new Team { Name = "Team1" },
                new Team { Name = "Team2" },
                new Team { Name = "Team3" }
            }.AsQueryable().BuildMockDbSet();

            var mockSet = new Mock<DbSet<Team>>(data);
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql("witam").Options;
            var myProfile = new TeamMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);
            var loggerMock = new Mock<ILogger<TeamServices>>();
            var authenticationSettingsMock = new Mock<AuthenticationSettings>();
            var mockContext = new Mock<AppDbContext>(dbOptions);

            mockContext.Setup(c => c.Team).Returns(data.Object);

            var service = new TeamServices(mockContext.Object, mapper, loggerMock.Object,
                authenticationSettingsMock.Object);
            var query = new DataBase.Models.Paination.Query { PageSize = 4, PageNumber = 2 };
            var result = await service.GetAll(query);

            result.Items.Count.Should().Be(query.PageSize);
            result.Items.Should().NotBeEmpty();
            result.Items.Should().Contain(result.Items.Skip(3));
        }
    }
}