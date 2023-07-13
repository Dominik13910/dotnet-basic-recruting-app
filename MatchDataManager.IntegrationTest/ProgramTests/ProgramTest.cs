using FluentAssertions;
using MatchDataManager.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using NLog.Config;
using System.Net.Http;

namespace MatchDataManager.IntegrationTest.ProgramTests
{
    public class ProgramTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private List<Type> _controllerTypes;
        private WebApplicationFactory<Program> _factory;

        public ProgramTest()
        {
            _controllerTypes = typeof(Program)
               .Assembly
               .GetTypes()
               .Where(t => t.IsSubclassOf(typeof(ControllerBase)))
           .ToList();

            _factory = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder =>
               {
                   builder.ConfigureServices(services =>
                   {
                       _controllerTypes.ForEach(c => services.AddScoped(c));
                   });
               });
        }

        [Fact]
        public void ConfigureServices_ForControllers_RegistersAllDependencies()
        {
            var scoopFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scoopFactory.CreateScope();

            _controllerTypes.ForEach(t =>
            {
                var controller = scope.ServiceProvider.GetService(t);
                controller.Should().NotBeNull();
            });
        }

    }
}
