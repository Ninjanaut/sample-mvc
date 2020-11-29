using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Mvc.Tests
{
    // By default, xUnit creates a new instance of a test class, for each test method.
    // Fixture makes only one shared instance that methods will reuse.
    // That is more efficient, when setup or teardown is expensive.
    public class HealthCheckTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        public HealthCheckTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateDefaultClient();
        }

        [Fact]
        public async Task HealthCheck_ReturnsOk()
        {
            // Arrange
            // We test that application can start properly.
            // * Server is running and can handle requests
            // * Required services registered with the DI container
            // * Middleware pipeline is correctly configured
            // * Routing sends requests to the expected endpoint

            // Act
            var response = await _httpClient.GetAsync("/healthcheck");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
