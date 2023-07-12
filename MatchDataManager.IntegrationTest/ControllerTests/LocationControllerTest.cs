using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MatchDataManager.Api.Models;
using FluentAssertions.Common;
using MatchDataManager.Api.Dto.Location;
using MatchDataManager.Api.Dto.Team;
using MatchDataManager.IntegrationTest.Helpers;

namespace MatchDataManager.IntegrationTest.ControllerTests
{
    public class LocationControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient _httpClient;

        public LocationControllerTest()
        {
            var factory = new WebApplicationFactory<Program>();
            _httpClient = factory.CreateClient();

        }


        [Fact]
        public async Task GetAll_WithQueryParameters_ReturnOkResult()
        {

            //act

            var response = await _httpClient.GetAsync("https://localhost:7234/Locations");
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Deleted_WithQueryParameters_ReturnOkResult()
        {

            //act

            var response = await _httpClient.DeleteAsync("https://localhost:7234/Locations/");
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetById_WithQueryParameters_ReturnOkResult()
        {

            //act

            var response = await _httpClient.GetAsync("https://localhost:7234/Locations/42fbf5e0-b339-4514-9e11-9bdc15aee459");
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_WithQueryParameters_ReturnCreated()
        {
            //arrange
            var model = new CreateLocationDto()
            {
                Name = "Gace",
                City = "City"
            };
            var httpContent = model.ToJsonHttpContent();

            //act

            var response = await _httpClient.PostAsync("https://localhost:7234/Locations/", httpContent);
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }
        [Fact]
        public async Task Update_WithQueryParameters_ReturnOkResult()
        {
            //arrange
            var model = new UpdateLocationDto()
            {
                Name = "Names",
                City = "city"
            };
            var httpContent = model.ToJsonHttpContent();

            //act

            var response = await _httpClient.PutAsync("https://localhost:7234/Locations?id=42fbf5e0-b339-4514-9e11-9bdc15aee459", httpContent);
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }




    }
}
