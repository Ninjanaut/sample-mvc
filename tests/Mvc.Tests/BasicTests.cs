using Microsoft.AspNetCore.Mvc.Testing;
using Mvc.Tests.TestingTools;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Mvc.Tests
{
    // By default, xUnit creates a new instance of a test class, for each test method.
    // Fixture makes only one shared instance that methods will reuse.
    // That is more efficient, when setup or teardown is expensive.
    public class BasicTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        public BasicTests(WebApplicationFactory<Startup> factory)
        {
            // The default environment is "Development"
            _httpClient = factory.CreateDefaultClient();
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/home/index")]
        [InlineData("/order/create")]
        public async Task GetServerSideRenderedPage_ReturnsOk(string url)
        {
            // Act
            var response = await _httpClient.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            Assert.Equal("text/html", response.Content.Headers.ContentType.MediaType);
            Assert.True(response.Content.Headers.ContentLength > 0);
        }

        [Fact]
        public async Task GetHomePage_EnsureThatWelcomeHeaderIsDisplayed()
        {
            // Act
            var response = await _httpClient.GetAsync("/");

            // Assert
            using var content = await HtmlHelpers.GetDocumentAsync(response);

            var h1 = content.QuerySelector("h1").TextContent;

            Assert.Equal("Welcome", h1);
        }
    }
}
