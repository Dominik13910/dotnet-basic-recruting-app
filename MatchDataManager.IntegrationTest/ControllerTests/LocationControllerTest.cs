using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MatchDataManager.DataBase.Models;
using FluentAssertions.Common;
using MatchDataManager.DataBase.Dto.Location;
using MatchDataManager.DataBase.Dto.Team;
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

        //GetAll Action

        [Fact]
        public async Task GetAll_WithQueryParameters_ReturnOkResult()
        {
            //act
            var response = await _httpClient.GetAsync("https://localhost:7234/Locations");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_WithQueryParameters_ReturnBadRequest()
        {
            //act
            var response = await _httpClient.GetAsync("api/Locations");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        //Deleted Action

        [Fact]
        public async Task Deleted_WithQueryParameters_ReturnOkResult()
        {
            //act
            var response =
                await _httpClient.DeleteAsync("https://localhost:7234/Locations/42fbf5e0-b339-4514-9e11-9bdc15aee459");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Deleted_WithQueryParameters_ReturnNotFound()
        {
            //act
            var response = await _httpClient.DeleteAsync("api/localhost:7234/Locations/");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        //GetById Action

        [Fact]
        public async Task GetById_WithQueryParameters_ReturnOkResult()
        {
            //act
            var response =
                await _httpClient.GetAsync("https://localhost:7234/Locations/42fbf5e0-b339-4514-9e11-9bdc15aee459");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_WithQueryParameters_ReturnNotFound()
        {
            //act
            var response =
                await _httpClient.GetAsync("https://localhost:7234/Locations/42fbf5e0-b339-4514-9e11-9bdc15aee333");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        //Created Action

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
        public async Task Create_WithQueryParameters_ReturnNotFound()
        {
            //arrange
            var model = new CreateLocationDto()
            {
                Name = "Gace",
                City = "City"
            };
            var httpContent = model.ToJsonHttpContent();

            //act
            var response = await _httpClient.PostAsync("api/location", httpContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_WithQueryParameters_ReturnBadRequest()
        {
            //arrange
            var model = new CreateLocationDto()
            {
                Name = "Gace",
                City = "City"
            };
            var httpContent = model.ToJsonHttpContent();

            //act
            var response = await _httpClient.PostAsync("api/location", httpContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        //Update Action
        [Fact]
        public async Task Update_WithQueryParameters_ReturnOkResult()
        {
            //arrange
            var model = new
            {
                Name = "Names",
                City = "City"
            };
            var httpContent = model.ToJsonHttpContent();

            //act
            var response =
                await _httpClient.PutAsync("https://localhost:7234/Locations?id=42fbf5e0-b339-4514-9e11-9bdc15aee459",
                    httpContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Update_WithQueryParameters_ReturnNotFound()
        {
            //arrange
            var model = new CreateLocationDto()
            {
                Name = "Names",
                City = "City"
            };
            var httpContent = model.ToJsonHttpContent();

            //act
            var response = await _httpClient.PutAsync("apiLocations", httpContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}