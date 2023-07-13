using FluentAssertions;
using MatchDataManager.Api.Models;
using MatchDataManager.Api.Models.Paination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchDataManager.UnitTest.Pagination
{
    public class PaginationTest
    {
        [Fact]
        public void GetTeam_CorrectlyAppliesPagination()
        {
            // Arrange
            var teams = new List<Team>
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

        }.AsQueryable();

            var query = new Query { PageSize = 2, PageNumber = 3};

            // Act
            var result = GetTeam(teams, query);

            // Assert
            result.Should().HaveCount(query.PageSize);
            result.Should().Contain(teams.Skip(query.PageSize * (query.PageNumber - 1)).Take(query.PageSize));
        }
      

        private List<Team> GetTeam(IQueryable<Team> teams, Query query)
        {
            return teams
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();
        }

      
   
   
    }
}
