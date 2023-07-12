using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using FluentAssertions;
using MatchDataManager.Api.Controllers;
using NLog.Config;
using MatchDataManager.Api.Dto.Location;
using MatchDataManager.Api.Dto.Team;
using Microsoft.EntityFrameworkCore;
using MatchDataManager.Api.Models;
using Moq;
using MatchDataManager.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MatchDataManager.IntegrationTest.Helpers;

namespace MatchDataManager.IntegrationTest.ControllerTests
{
    public class TeamControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient _httpClient;


        public TeamControllerTest()
        {

            _httpClient = new WebApplicationFactory<Program>()

                /* .WithWebHostBuilder(builder =>
                 {
                     builder.ConfigureServices(services =>
                     {
                         var dbContextOption = services
                         .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<AppDbContext>));
                         services.Remove(dbContextOption);
                         services.AddDbContext<AppDbContext>(OptionsBuilderConfigurationExtensions => OptionsBuilderConfigurationExtensions.UseInMemoryDatabase("DataBase"));
                     });
                 })*/
                .CreateClient();

        }

        [Theory]
        [InlineData("PageNumber=1&PageSize=5")]
        [InlineData("SerchName=Grey&PageNumber=1&PageSize=5")]
        public async Task GettAll_WithQueryParameters_ReturnOkResult(string queryParams)
        {
            //arange

            //act

            var response = await _httpClient.GetAsync("https://localhost:7234/Teams?" + queryParams);
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        public async Task Deleted_WithQueryParameters_ReturnOkResult()
        {

            //act

            var response = await _httpClient.DeleteAsync("https://localhost:7234/Teams/");
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }



        [Fact]
        public async Task GetById_WithQueryParameters_ReturnOkResult()
        {

            //act

            var response = await _httpClient.GetAsync("https://localhost:7234/Teams/7da22f0a-d22f-4127-86e9-a10f2e6004e6");
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        public async Task Create_WithQueryParameters_ReturnOkResult()
        {
            //arrange
            var model = new CreateTeamDto()
            {
                Name = "Namae",
                CoachName = "CoachName"
            };
            var httpContent = model.ToJsonHttpContent();

            //act

            var response = await _httpClient.PostAsync("https://localhost:7234/Teams/", httpContent);
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }
        [Fact]
        public async Task Update_WithQueryParameters_ReturnCreated()
        {
            //arrange
            var model = new UpdateTeamDto()
            {
                Name = "Hamay",
                CoachName = "Damy"
            };
            var httpContent = model.ToJsonHttpContent();

            //act

            var response = await _httpClient.PutAsync("https://localhost:7234/Teams?id=7da22f0a-d22f-4127-86e9-a10f2e6004e6", httpContent);
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

    }
}
